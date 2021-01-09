import React, { Component } from 'react';
import { Layout } from './Layout';
import LogIn from './User/LogIn';
import routes from './routes';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link,
    useRouteMatch,
    useParams
} from "react-router-dom";
export default class Main extends Component {
    static displayName = Main.name;

    render() {
        return (<div>
            <Route exact path='/login' component={LogIn} />
            <Layout>
                <Switch>
                    {routes.map((prop, key) => <Route exact path={prop.path} component={prop.component} />)}
                </Switch>
            </Layout>
            </div>
        );
    }
}