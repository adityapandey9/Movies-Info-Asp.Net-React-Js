import React, {Component} from 'react';
import { withRouter } from 'react-router-dom';
import * as routes from '../routes/routes';
import { URL } from '../config';
import alertify from 'alertifyjs';

class Tags extends Component {

    constructor(props){
        super(props);
        this.state = {data: null, type: 2}
    }

    componentDidMount(){
        let type = 0;
        let id = 0;
        if(window.location.href.indexOf("actors") > -1){
            type = 1;
            id = parseInt(window.location.pathname.replace("/actors/", ""))
        } else if(window.location.href.indexOf("producers") > -1) {
            type = 0;
            id = parseInt(window.location.pathname.replace("/producers/", ""))
        }
        if(this.props.location.state == null){
            this.setState({type: type}, ()=>{
                this.getData(id, type)
            });
            return;
        }
        this.setState({data: this.props.location.state});
    }

    getData(id, type){
        let url = URL+`/api/movies/producers/`+id;
        if(type == 1){
            url = URL+`/api/movies/actors/`+id;
        }
        this.fetch(url, {method: 'GET'}).then((res)=>{
            res.json().then((data)=>{
                this.setState({data: data});
            });
        }).catch((err)=>{
            alertify.error("Error Occured, Could not get the Data")
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
    
    render(){
        if(this.state.data == null)
            return <div className="loader"></div>;
        
        const data = this.state.data;
        return (
            <div>
                <div className="uk-card uk-card-default">
                    <div className="uk-card-header">
                        <div className="uk-grid-small uk-flex-middle" uk-grid="true">
                            <div className="uk-width-expand">
                                <h3 className="uk-card-title uk-margin-remove-bottom">{data.name} ({data.sex==1?"Male":"Female"})</h3>
                                <p className="uk-text-meta uk-margin-remove-top"><b>DoB: </b><time dateTime={data.dob}>{new Date(data.dob).toDateString()}</time></p>
                            </div>
                        </div>
                    </div>
                    <div className="uk-card-body">
                        <p><b>Bio: </b>{data.bio}</p>
                    </div>
                </div>

                <style jsx="true">{`
                    .card-footer {
                        padding: 1px 1px;
                    }
                `}
                </style>
            </div>
        );
    }
}


export default withRouter(Tags);