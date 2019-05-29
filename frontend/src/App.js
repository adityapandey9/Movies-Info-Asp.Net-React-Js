import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Route,
  Link,
  Switch
} from "react-router-dom";
import logo from './images/logo.png';
import * as routes from './routes/routes';
import Index from './pages';
import EditMovies from './pages/editmovies';
import Tags from './pages/user';
import Movies from './pages/movies';
import './app.css';

class App extends Component {

  constructor(props){
    super(props);
  }

  render() {
    return (
      <Router>
                <div className="uk-panel">
                    <nav className="uk-navbar-container uk-margin" style={{background: "#2f3239"}} uk-navbar="true">
                        <div className="uk-navbar-left">
                            <Link className="uk-navbar-item uk-logo" to={routes.INDEX}><img src={logo} /></Link>
                        </div>
                        <div className="uk-navbar-right">
                            <Link className="uk-button uk-button-primary uk-margin-small-left uk-margin-small-right" style={{backgroundColor: "#ff7361"}} to={routes.ADDMOVIES}>Add Movie</Link>
                        </div>
                        
                    </nav>
                    <div className="mainbody">
                     <Switch>
                      <Route 
                        exact path={routes.INDEX}
                        component={() => <Index /> }
                      />
                      <Route
                        path={routes.PRODUCER}
                        component={()=> <Tags />}
                      />
                      <Route
                        path={routes.MOVIES}
                        component={()=> <Movies /> }
                      />
                      <Route
                        path={routes.EDITMOVIES}
                        component={()=> <EditMovies /> }
                      />
                      <Route
                        path={routes.ADDMOVIES}
                        component={()=> <EditMovies /> }
                      />
                      <Route
                        path={routes.ACTOR}
                        component={()=> <Tags /> }
                      />
                    </Switch>
                    </div>
                    <div className="footer" uk-sticky="bottom: true">

                    </div>
                    <style jsx="true">{`
                        .mainbody {
                            margin-left: 10%;
                            margin-right: 10%;
                            margin-top: 5%;
                        }
                        
                    `}
                    </style>
                </div>
            </Router>
    );
  }
}

export default App;
