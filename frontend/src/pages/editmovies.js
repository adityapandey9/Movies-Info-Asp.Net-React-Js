import React, {Component} from 'react';
import { withRouter, Redirect } from 'react-router-dom';
import fetch from 'isomorphic-fetch';
import * as Config from '../config';
import alertify from 'alertifyjs';

//EditMovies Compoent for Movies
class EditMovies extends Component {
    
    constructor(props){
        super(props);
        this.state = {
            moviesId: 0,
            name: '', 
            year: 0, 
            plot: '',
            poster: '',
            response: {}, 
            isloaded: true,
            producerId: 0,
            producername:"",
            producersex:1,
            producerdob:"",
            producerbio:"",
            iserroron: true,
            moivesActors: [],
            producerData: [],
            actorsData: [],
        };
        this.actors = {
            actorId: 0,
            name: '',
            bio: '',
            sex: 1,
            dob: '',
            type: 0
        }
        this.fetch = this.fetch.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.nameInput = null;
        this.isopen = false;
        this.getData();
    }

    componentDidMount(){
        //Check the Url and load the data According
        if(window.location.href.indexOf("editmovies")> -1 && this.props.location.state != null){
            let obj = this.props.location.state;
            this.setPAData(obj);   
        } else {
            this.setState({moivesActors: [Object.assign({}, this.actors)]});
        }
    }

    //Set the State of the Compoent from the Response
    setPAData = (obj) => {
        var moivesActors = [...obj.moivesActors];
        delete obj.moivesActors;
        var date = new Date(obj.producers.dob);
        var pros = {producerId: obj.producers.producerId,
            producername: obj.producers.name,
            producersex: obj.producers.sex,
            producerdob: new Date(date.getFullYear(), date.getMonth(), date.getDate()+1).toISOString().slice(0, 10),
            producerbio: obj.producers.bio
        };
        delete obj.producers;
        for(let i=0;i<moivesActors.length;i++){
            moivesActors[i] = {...moivesActors[i].actors};
            moivesActors[i].type = 0;
            date = new Date(moivesActors[i].dob);
            moivesActors[i].dob = new Date(date.getFullYear(), date.getMonth(), date.getDate()+1).toISOString().slice(0, 10);
        }
        this.setState({...obj, moivesActors: moivesActors, ...pros})
    }

    //Check if it is EditMovie Path or not if yes return the Id
    isEditMovie = () => {
        if(window.location.href.indexOf("editmovies")> -1 && this.props.location.state != null){
            return this.props.location.state.moviesId;
        }
        else if(window.location.href.indexOf("editmovies")> -1 && this.props.location.state == null)
        {
            return parseInt(window.location.pathname.replace("/editmovies/", ""));
        }
        return -1;
    };

    //Get the Response Accordingly
    getData = () => {
        var pros = [];
        pros.push(this.fetch(Config.URL+`/api/movies/pa`));
        if(this.isEditMovie() > 0 && this.props.location.state == null)
            pros.push(this.fetch(Config.URL+`/api/movies/movie/`+this.isEditMovie()));
        Promise.all(pros).then((val)=>{
                val[0].json().then(json=>{
                    this.setState({actorsData: json.actor, producerData: json.producer});
                }).catch(er => {
                    alertify.error("Error Occured, Could not get the Actors and Producers List")
                    this.setState({actorsData: [{name: "Some Error Occured", actorId: -1}], producerData: [{name: "Some Error Occured", producerId: -1}]});  
                })
                if(this.isEditMovie() > 0 && this.props.location.state == null){
                    val[1].json().then(json=>{
                        this.setPAData(json);
                        // this.setState({...json});
                    }).catch(er => {
                        alertify.error("Error Occured, Could not get the Movie Info")
                        this.setState({moviesData: [{name: "Some Error Occured", moviesId: -2}]});                    
                    })
                }
        }).catch((err)=>{
            alertify.error("Error Occured, Could not get the Data Require")
            this.setState({actorsData: [{name: "Some Error Occured", actorId: -1}], producerData: [{name: "Some Error Occured", producerId: -1}], moviesData: [{name: "Some Error Occured", moviesId: -1}]});      
        });
    }

    //Create a Post Request Model for Sending the Data
    setPostData = (obj) => {
        var obj = Object.assign({}, obj);
        var moivesActors = [...obj.moivesActors];
        delete obj.moivesActors;
        delete obj.actorsData;
        delete obj.isloaded;
        delete obj.moviesData;
        delete obj.producerData;
        var pros = {producerId: obj.producerId,
            name: obj.producername,
            sex: obj.producersex,
            dob: new Date(obj.producerdob).toJSON(),
            bio: obj.producerbio
        };
        delete obj.producerId;
        delete obj.producerbio;
        delete obj.producerdob;
        delete obj.producername;
        delete obj.producersex;
        delete obj.response;

        for(let i=0;i<moivesActors.length;i++){
            moivesActors[i] = {actors: Object.assign({}, moivesActors[i])};
            moivesActors[i].actors.dob = new Date(moivesActors[i].actors.dob).toJSON();
            delete moivesActors[i].actors.type;
        }
        obj["moivesActors"] = moivesActors;
        obj["producers"] = pros;
        return obj;
    }

    //Handle the Submit Event
    handleSubmit(event) {
        event.preventDefault();
        if (!event.target.checkValidity() || !this.validate()) {
            // form is invalid! so we do nothing
            return;
        }
        var obj = this.setPostData(this.state);
        console.log("skd: ", obj);
        //Set the isSubmitted true for showing the response to the user
        this.setState({isloadedd: true}, ()=>{
            this.fetch(Config.URL+`/api/movies`, {
                method: 'POST',
                body: JSON.stringify(obj)
              }).then(res => {
                  res.json().then(data=>{
                    console.log(data);
                    this.setState({response: data});
                  }).catch(err=>{
                      this.setState({response: err});
                  })
                  alertify.alert("Data, Saved Successfully");
              }).catch(err => {
                // this.setState({response: err});
                alertify.error("Error Occured, Could not save the Data")
            });
        });
   }

    fetch(url, options){
        // performs api calls sending the required authentication headers
        const headers = {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
    
        return fetch(url, {
          headers,
          ...options
        })
    }

    //Handle Change for the Different Entity
    handleChange = (event) => {
        event.persist();
        if(event.target.name == 'poster'){
            this.imageExists(event.target.value);
        }
        this.setState({[event.target.name]: event.target.value}, ()=>{
            if(event.target.name == 'poster' && this.nameInput != null){
                this.nameInput.focus();
            }
        })
    };

    //Change the Cast box by ID
    handleChangeCast = (event, i) => {
        var obj = this.state.moivesActors;
        obj[i][event.target.name] = event.target.value;
        this.setState({moivesActors: obj});
    };

    //Add the Cast Box
    addCast = (event, i, e) => {
        event.preventDefault();
        //Add New Cast Info
        if(e == 2){
            var obj = this.state.moivesActors;
            obj[i]["type"] = 1;
            this.setState({moivesActors: obj});
        } 
        //Select the Cast from the List
        else if(e == 1 && event.target.value != -1) 
        {
            var obj = this.state.moivesActors;
            var type = obj[i].type;
            obj[i] = this.state.actorsData[parseInt(event.target.selectedOptions[0].attributes.val.value)];
            obj[i].type = type;
            var date = new Date(obj[i].dob);
            obj[i].dob = new Date(date.getFullYear(), date.getMonth(), date.getDate()+1).toISOString().slice(0, 10)
            this.setState({moivesActors: obj});
        } 
        //Select the Producer by the List
        else if(e == 3 && event.target.value != -1){
            var pros = this.state.producerData[parseInt(event.target.selectedOptions[0].attributes.val.value)];
            var obj = {
                producerId: pros.producerId,
                producername: pros.name,
                producersex: pros.sex,
                producerdob: new Date(pros.dob).toISOString().slice(0, 10),
                producerbio: pros.bio
            };
            this.setState({...obj});
        } 
        //Add a New Producer
        else if(e == 4){
            this.isopen = true;
            this.forceUpdate();
        }
    }

    //Close Cast Box
    closeCast = (e, i) => {
        e.preventDefault();
        var obj = this.state.moivesActors;
        obj.splice(i, 1);
        this.setState({moivesActors: obj});
    }

    //Cast Info, I the location in the Array, pa type of List -- Cast or Producer
    getCastUI = (cast, i, pa) => {
        let options = [];
        let val = null;
        if(pa == 0){
            val = this.state.moivesActors[i].actorId;
            for(let i=0;i<this.state.actorsData.length;i++){
                options.push(<option value={this.state.actorsData[i].actorId} val={i} key={i}>{this.state.actorsData[i].name}</option>);
            }
        } else if(pa == 1) {
            val = this.state.producerId;
            for(let i=0;i<this.state.producerData.length;i++){
                options.push(<option value={this.state.producerData[i].producerId} val={i} key={i}>{this.state.producerData[i].name}</option>);
            }
        }
        return (<div className="uk-margin uk-card uk-card-default uk-card-body" key={i}>
       {(pa == 0 && this.state.moivesActors.length > 1) && <div onClick={e => this.closeCast(e, i)} className="uk-card-badge uk-label poi">X</div> }
       {(pa == 1 && this.isopen == true) && <div onClick={e => { this.isopen=false;this.forceUpdate()}} className="uk-card-badge uk-label poi">Back</div>}
       { pa == 0 && <h3 className="uk-card-title" style={{textAlign: "center"}}>Enter Cast Details</h3> }
       { pa == 1 && <h3 className="uk-card-title" style={{textAlign: "center"}}>Enter Producer Details</h3> }
        { (cast.type == 0 || (this.isopen == false && pa == 1)) && 
        <div uk-margin="true" style={{textAlign: "center"}}>
            <div className="uk-form-controls" style={{width: "53%"}}>
                <select className="uk-select" onChange={(e)=>{
                    if(pa == 0)
                        this.addCast(e, i, 1)
                    else if(pa == 1)
                        this.addCast(e, i, 3)
                    }} value={val}>
                    <option value="-1">Select From Avaliable Cast</option>
                    {options}
                </select>
            </div>
            <p className="uk-margin">OR</p>
            <p onClick={(e)=>{
                if(pa == 0)
                    this.addCast(e, i, 2);
                else if(pa == 1)
                    this.addCast(e, i, 4)
                }} name="1" className="uk-button uk-button-default">{pa == 0?"Add A New Cast":"Add A New Producer"}</p>
        </div> }
        { (cast.type == 1 && pa == 0) &&  
            <div>
                <div className="uk-margin">
                    <label className="uk-form-label" htmlFor="form-horizontal-text">Cast Name</label>
                    <div className="uk-form-controls">
                        <input className="uk-input" id="form-horizontal-text" value={this.state.moivesActors[i].name} name="name" type="text" onChange={(e)=>this.handleChangeCast(e, i)} placeholder="Cast Name" required/>
                    </div>
                </div>
                <div className="uk-margin">
                    <div className="uk-form-label">Cast Sex</div>
                    <div className="uk-form-controls uk-form-controls-text">
                        <label><input className="uk-checkbox" type="checkbox" onChange={(e)=>this.handleChangeCast(e, i)} name="sex" value="1" checked={cast.sex == 1} /> Male </label>
                        <label><input className="uk-checkbox" type="checkbox" onChange={(e)=>this.handleChangeCast(e, i)} name="sex" value="2" checked={cast.sex == 2} /> Female</label>
                    </div>
                </div>
            
                <div className="uk-margin">
                    <label className="uk-form-label" htmlFor="form-horizontal-text">Cast DoB</label>
                    <div className="uk-form-controls">
                        <input type="date" className="uk-input" id="form-horizontal-text" value={this.state.moivesActors[i].dob} name="dob" onChange={(e)=>this.handleChangeCast(e, i)} required/>
                    </div>
                </div>
                <div className="uk-margin">
                    <textarea className="uk-textarea"  value={this.state.moivesActors[i].bio} name="bio" onChange={(e)=>this.handleChangeCast(e, i)} onFocus={()=>{this.setState({iserroron: false})}} onBlur={(e)=>{this.setState({iserroron: true})}} rows="5" placeholder="Enter Bio of the Cast" required></textarea>
                </div>
            </div>
        }
        {(pa == 1 && this.isopen == true) && 
            <div>
                 <div className="uk-margin">
                                <label className="uk-form-label" htmlFor="form-horizontal-text">Producer Name</label>
                                <div className="uk-form-controls">
                                    <input className="uk-input" id="form-horizontal-text" value={this.state.producername} name="producername" type="text" onChange={this.handleChange} placeholder="Producer Name" required/>
                                </div>
                            </div>
                            <div className="uk-margin">
                                <div className="uk-form-label">Producer Sex</div>
                                <div className="uk-form-controls uk-form-controls-text">
                                    <label><input className="uk-radio" type="radio" onChange={this.handleChange} name="producersex" value="1" checked={this.state.producersex == 1} /> Male </label>
                                    <label><input className="uk-radio" type="radio" onChange={this.handleChange} name="producersex" value="2" checked={this.state.producersex == 2} /> Female</label>
                                </div>
                            </div>
                            <div className="uk-margin">
                                <label className="uk-form-label" htmlFor="form-horizontal-text">Producer DoB</label>
                                <div className="uk-form-controls">
                                    <input type="date" className="uk-input" id="form-horizontal-text" value={this.state.producerdob} name="producerdob" onChange={this.handleChange} required/>
                                </div>
                            </div>
                            <div className="uk-margin">
                                <textarea className="uk-textarea"  value={this.state.producerbio} name="producerbio" onChange={this.handleChange} onFocus={()=>{this.setState({iserroron: false})}} onBlur={(e)=>{this.setState({iserroron: true})}} rows="5" placeholder="Enter Bio of the Producer" required></textarea>
                            </div>
            </div>}
    </div>);

    }

    //Add An Empty Cast Box
    addNewCast = (event) => {
        event.preventDefault();
        var obj = Object.assign({}, this.actors);
        this.setState({moivesActors: [...this.state.moivesActors, obj]});
    }

    //Check Validation for Different things
    validate = () => {
        const {
            name,
            plot,
            year,
            poster,
            moivesActors,
            producername,
            producerbio,
            producersex,
            producerdob,
            isloaded
          } = this.state;
        
        if(this.nameInput == null || !this.state.iserroron)
          return;
        
        var regex = /^[a-zA-Z \.]{2,30}$/;

        if(name === ''){
            alertify.error("Movie Name is Empty");
            return false;
        }
        if(plot === ''){
            alertify.error("Movie Plot is Empty");
            return false;
        }
        if(year < 1900){
            alertify.error("Movie Year is Lower than 1900");
            return false;
        }

        if(poster === '' || isloaded == false){
            alertify.error("Movie Poster is wrong");
            return false;
        }
        if(producername === '' || (producername != '' && !regex.test(producername))){
            alertify.error("Movie Producer Name is Empty or Wrong");
            return false;
        }
        if(producerbio === ''){
            alertify.error("Movie Producer Bio is Empty");
            return false;
        }
        if(producerdob === '' || (producerdob != '' && new Date(producerdob).getTime() > new Date().getTime())){
            alertify.error("Movie Producer DOB is Empty or Wrong");
            return false;
        }
        if(producersex == 0){
            alertify.error("Movie Producer Sex is Empty");
            return false;
        }
        if(moivesActors.length > 0){
            for(let i=0;i<moivesActors.length;i++){
                if(moivesActors[i].name == '' || (moivesActors[i].name != '' && !regex.test(moivesActors[i].name))){
                    alertify.error("Movie Cast Name is Empty or Wrong: "+(i+1));
                    return false;
                }
                if(moivesActors[i].bio == ''){
                    alertify.error("Movie Cast Bio is Empty: "+(i+1));
                    return false;
                }
                if(moivesActors[i].dob == '' || (moivesActors[i].dob != '' && new Date(moivesActors[i].dob).getTime() > new Date().getTime() )){
                    alertify.error("Movie Cast Bio is Empty or Wrong: "+(i+1));
                    return false;
                }
                if(moivesActors[i].sex == 0){
                    alertify.error("Movie Cast Sex is Empty: "+(i+1));
                    return false;
                }
            }
        }
        return true;
    }

    imageExists = (image_url) =>{
 
        try {
            var http = new XMLHttpRequest();
        
            http.open('HEAD', image_url, false);
            http.send();
            if(http.status == 200){
                this.setState({isloaded: true})
            } else {
                this.setState({isloaded: false})
            }
        } catch(e){
            this.setState({isloaded: false})
        }
    }

    render(){

        const {
            name,
            plot,
            year,
            poster
          } = this.state;
      
        const isInvalid = !this.validate();
        
        let casts = [];
        for(let i=0;i<this.state.moivesActors.length;i++){
            casts.push(this.getCastUI(this.state.moivesActors[i], i, 0));
        }

        return (
            <div>
               <form className="uk-form-horizontal uk-margin-large" onSubmit={this.handleSubmit}>
                            <h3 style={{textAlign: "center"}}>Movies Details</h3>
                            <div className="uk-margin">
                                <label className="uk-form-label" htmlFor="form-horizontal-text">Name</label>
                                <div className="uk-form-controls">
                                    <input className="uk-input" id="form-horizontal-text" value={name} name="name" type="text" onChange={this.handleChange} placeholder="Movie Name" required/>
                                </div>
                            </div>
                            <div className="uk-margin">
                                <label className="uk-form-label" htmlFor="form-horizontal-text">Year</label>
                                <div className="uk-form-controls">
                                    <input className="uk-input" id="form-horizontal-text" value={year} name="year" type="number" onChange={this.handleChange} required/>
                                </div>
                            </div>
                        {poster != '' &&
                            <div className="uk-grid-collapse uk-child-width-1-2@s uk-margin" uk-grid="true">
                                 {poster != ''}
                                <div className="uk-card-media-right uk-cover-container">
                                    <img src={poster} alt="" uk-cover="true" />
                                    <canvas width="600" height="400"></canvas>
                                </div> 
                                <div>
                                    <div className="uk-card-body">
                                        <p>Poster</p>
                                        <div>
                                            <input ref={(input) => { this.nameInput = input; }} className="uk-input" id="form-horizontal-text" value={poster} name="poster" type="url" onChange={this.handleChange} placeholder="Enter Poster of the Movie" required/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            }
                            {poster == '' &&
                            <div className="uk-margin">
                                <label className="uk-form-label" htmlFor="form-horizontal-text">Poster</label>
                                <div className="uk-form-controls">
                                    <input className="uk-input" id="form-horizontal-text" value={poster} name="poster" type="url" onChange={this.handleChange} placeholder="Enter Poster of the Movie" required/>
                                </div>
                            </div>
                            }
                            <div className="uk-margin">
                                <textarea className="uk-textarea"  value={plot} name="plot" onChange={this.handleChange} onFocus={()=>{this.setState({iserroron: false})}} onBlur={(e)=>{this.setState({iserroron: true})}} rows="5" placeholder="Plot of the Movie" required></textarea>
                            </div>
                            {this.getCastUI({type: 3}, 0, 1)}
                            <h3 style={{textAlign: "center"}}>Movie Cast</h3>
                            <p onClick={this.addNewCast} className="uk-button uk-button-primary uk-button-small">Add More Cast</p>
                            
                            {casts}

                            <div className="uk-margin" style={{textAlign: "center"}}>
                                <button type="submit" disabled={isInvalid} className="uk-button uk-button-primary">Submit</button>
                            </div>
                </form>
            </div>
        );
    }
}


export default withRouter(EditMovies);