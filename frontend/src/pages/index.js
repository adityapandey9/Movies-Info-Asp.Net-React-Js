import React, {Component} from 'react';
import { withRouter, Link } from 'react-router-dom';
import * as myp from './data';
import * as routes from '../routes/routes';
import fetch from 'isomorphic-fetch'
import { URL } from '../config';
import alertify from 'alertifyjs';

class New extends Component {
    
    constructor(props){
        super(props);
        this.state = {data: myp.Posts}
        this.gotoMyPro = this.gotoMyPro.bind(this);
        this.fetch = this.fetch.bind(this);
        this.getData = this.getData.bind(this);
        this.getData();
    }

    gotoMyPro(id, val, pos, index){
        const {
            history,
          } = this.props;
        
        switch(val){
            case 1: 
                history.push("/movies/"+id, {...this.state.data[pos]});
                break;
            case 2:
                history.push("/actors/"+id, {...this.state.data[pos].moivesActors[index].actors});
                break;
            case 3:
                history.push("/producers/"+id, {...this.state.data[pos].producers});
                break;
            case 4:
                history.push("/editmovies/"+id, {...this.state.data[pos]});
                break;
        }
    }

    getData(){
        this.fetch(URL+`/api/movies`, {method: 'GET'}).then((res)=>{
            res.json().then((data)=>{
                this.setState({data: data});
            });
        }).catch((err)=>{
            // window.alert(err);
            alertify.error("Error Occured, Could not get the Movies List")
        })
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


    getUI(data, pos){
        // console.log(data);
        let tags = [];
        for(let i=0;i<data.moivesActors.length;i++){
            tags.push(<span onClick={e=>{
                e.preventDefault();
                this.gotoMyPro(data.moivesActors[i].actors.actorId, 2, pos, i)
            }} className="uk-badge uk-margin-small-left poi" key={data.moivesActors[i].actors.actorId}>{data.moivesActors[i].actors.name}</span>);
        }

        return (
                <li key={data.moviesId}>
                    <div className="uk-card uk-card-default uk-grid-collapse uk-child-width-1-2@s uk-margin" uk-grid="true">
                        <div onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, pos, 0)
                    }} className="uk-card-media-left uk-cover-container" style={{cursor: "pointer"}}>
                            <img src={data.poster} alt="" uk-cover="true" />
                            <canvas width="600" height="400"></canvas>
                        </div>

                        <div>
                            <div className="uk-card-body">
                                <div onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 4, pos, 0)
                    }} className="uk-card-badge uk-label poi" style={{backgroundColor: "#ff7361", padding: '.4em', paddingLeft: '2.1em', paddingRight: '2.1em'}}>Edit</div>
                                <h3 onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, pos, 0)
                    }} className="uk-card-title poi">{data.name}</h3>
                                <div style={{fontSize: 12}}>
                                <p onClick={e=>{
                        e.preventDefault();
                        this.gotoMyPro(data.moviesId, 1, pos, 0)
                    }}>{data.plot}</p>
                                <p>
                                    <b>Release date: </b>
                                    <span>{data.year}</span>
                                </p>
                                <p>
                                    <b>Producers: </b>
                                    <span onClick={e=>{
                                        e.preventDefault();
                                        this.gotoMyPro(data.producers.producerId, 3, pos, 0)
                                    }} className="uk-badge uk-margin-small-left poi">{data.producers.name}</span>
                                </p>
                                <b>Cast: </b>
                                {tags}
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
        );
    }

    render(){
        if(this.state.data == null)
            return <div className="loader"></div>;
        let posts = [];
        const data = this.state.data;
        for(let i = 0;i<data.length;i++){
            posts.push(this.getUI(data[i], i));
        }
        return (
            <div>
                <ul className="uk-list">
                    {posts}
                </ul>
                <style jsx="true">{`
                    .card-footer {
                        padding: 1px 1px;
                    }
                    .loader {
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


export default withRouter(New);