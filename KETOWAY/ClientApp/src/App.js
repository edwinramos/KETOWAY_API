import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Main from './components/Main';
import LogIn from './components/User/LogIn';
import './custom.css'
import Cookies from 'js-cookie';

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        var isActiveUser = false;
        var user = Cookies.get('activeUser');
        if (user)
            isActiveUser = true;

        this.state = {
            isLogOn: isActiveUser, activeUser: []
        };

        this.onLogInSucess = this.onLogInSucess.bind(this);
    }

    onLogInSucess() {
        this.setState({
            isLogOn: true,
        });
    }

    render() {
        return (
            <div>
                {this.state.isLogOn ?
                    <Main /> :
                    <LogIn onLogInSucess={() => { this.onLogInSucess() }} />
                }
            </div>
        );
    }
}
