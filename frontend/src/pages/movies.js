import React, {Component} from 'react';
import { withRouter } from 'react-router-dom';
import * as routes from '../routes/routes';
import fetch from 'isomorphic-fetch';
import alertify from 'alertifyjs';

class Questions extends Component {

    constructor(props){
        super(props);
        this.state = {data: null, answer: '', update: true, vote: 0};
        this.getPro = this.getPro.bind(this);
    }

    componentDidMount(){
        this.getPro();
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

    getPro(){
        if(this.props.location.state == null){
            this.getData(parseInt(window.location.pathname.replace("/movies/", "")));
            return;
        }
        this.setState({data: this.props.location.state});
    }

    getData(id){
        let url = URL+`/api/movies/movie/`+id;
        this.fetch(url, {method: 'GET'}).then((res)=>{
            res.json().then((data)=>{
                this.setState({data: data});
            });
        }).catch((err)=>{
            alertify.error("Error Occured, Could not get the Data")
        })
    }

    gotoMyPro = (id, val, index) => {
        const {
            history,
          } = this.props;
        
        switch(val){
            case 1: 
                // history.push("/movies/"+id, {...this.state.data});
                break;
            case 2:
                history.push("/actors/"+id, {...this.state.data.moivesActors[index].actors});
                break;
            case 3:
                history.push("/producers/"+id, {...this.state.data.producers});
                break;
            case 4:
                history.push("/editmovies/"+id, {...this.state.data});
                break;
        }
    }

    render(){
        if(this.state.data == null)
            return <div className="loader"></div>;
        const data = this.state.data;
        let tags = [];
        for(let i=0;i<data.moivesActors.length;i++){
            tags.push(<span onClick={e=>{
                e.preventDefault();
                this.gotoMyPro(data.moivesActors[i].actors.actorId, 2, i)
            }} className="uk-badge uk-margin-small-left poi" key={data.moivesActors[i].actors.actorId}>{data.moivesActors[i].actors.name}</span>);
        }
        return (
            <div>
                <div className="uk-card uk-card-default uk-grid-collapse uk-child-width-1-2@s uk-margin" uk-grid="true">
                        <div onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, 0)
                    }} className="uk-card-media-left uk-cover-container" style={{cursor: "pointer"}}>
                            <img src={data.poster} alt="" uk-cover="true" />
                            <canvas width="600" height="400"></canvas>
                        </div>

                        <div>
                            <div className="uk-card-body">
                                <div onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 4, 0)
                    }} className="uk-card-badge uk-label poi" style={{backgroundColor: "#ff7361", padding: '.4em', paddingLeft: '2.1em', paddingRight: '2.1em'}}>Edit</div>
                                <h3 onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, 0)
                    }} className="uk-card-title poi">{data.name}</h3>
                                <div style={{fontSize: 12}}>
                                <p onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, 0)
                    }}>{data.plot}</p>
                                <p>
                                    <b>Release date: </b>
                                    <span>{data.year}</span>
                                </p>
                                <p>
                                    <b>Producers: </b>
                                    <span onClick={e=>{
                                        e.preventDefault();
                                        this.gotoMyPro(data.producers.producerId, 3, 0)
                                    }} className="uk-badge uk-margin-small-left poi">{data.producers.name}</span>
                                </p>
                                <b>Cast: </b>
                                {tags}
                                </div>
                            </div>
                        </div>
                    </div>
                <style jsx="true">{`
                    .card-footer {
                        padding: 1px 1px;
                    }
                    .newit {
                        width: 4%;
                    }
                    .loader {
                        border: 16px solid #f3f3f3;
                        border-radius: 50%;
                        border-top: 16px solid blue;
                        border-right: 16px solid green;
                        border-bottom: 16px solid red;
                        width: 120px;
                        height: 120px;
                        -webkit-animation: spin 2s linear infinite;
                        animation: spin 2s linear infinite;
                      }
                      
                      @-webkit-keyframes spin {
                        0% { -webkit-transform: rotate(0deg); }
                        100% { -webkit-transform: rotate(360deg); }
                      }
                      
                      @keyframes spin {
                        0% { transform: rotate(0deg); }
                        100% { transform: rotate(360deg); }
                      }
                `}
                </style>
            </div>
        );
    }
}


export default withRouter(Questions);