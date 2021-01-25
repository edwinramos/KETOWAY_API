import React, { Component } from 'react';
import Main from './components/Main';
import LogIn from './components/User/LogIn';
import './custom.css'
import { ToastContainer, Slide } from 'react-toastify';
import { getCookie } from './components/Helper';

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        var isActiveUser = false;
        var user = getCookie('activeUser');
        if (user)
            isActiveUser = true;

        this.state = {
            isLogOn: isActiveUser, activeUser: []
        };

        this.onLogInSucess = this.onLogInSucess.bind(this);
    }

    render() {
        return (
            <div>
                <ToastContainer
                    position="top-center"
                    transition={Slide}
                />
                {this.state.isLogOn ?
                    <Main /> :
                    <LogIn onLogInSucess={() => { this.onLogInSucess() }} />
                }
            </div>
        );
    }



    onLogInSucess() {
        this.setState({
            isLogOn: true,
        });
    }
}
