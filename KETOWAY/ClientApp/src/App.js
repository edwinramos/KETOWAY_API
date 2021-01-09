import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Main from './components/Main';
import LogIn from './components/User/LogIn';
import './custom.css'


export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            isLogOn: false,
        };
        this.onLogIn = this.onLogIn.bind(this);
    }

    onLogIn = (e) => {

        this.setState({
            isLogOn: true,
        });
    }

    render() {
        return (
            <div>
                {this.state.isLogOn ?
                    <Main /> :
                    <LogIn onLogIn={() => { this.onLogIn() }} />
                }
            </div>
        );
    }
}
