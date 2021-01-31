import React, { Component } from 'react';
import Main from './components/Main';
import LogIn from './components/User/LogIn';
import './custom.css'
import { ToastContainer, Slide } from 'react-toastify';
import { getCookie, setCookie } from './components/Helper';
import RecoverPassword from './components/User/RecoverPassword';

export default class App extends Component {
    static displayName = App.name;
    state = {
        isLogOn: false, isRecoverPassword: false, recoveryUserCode: ""
    };
    constructor(props) {
        super(props);
        var isActiveUser = false;
        var user = getCookie('activeUser');
        if (user)
            isActiveUser = true;

        var currentUrl = window.location.href;
        if (currentUrl.includes('recoverPassword')) {
            var userCode = currentUrl.split('/')[4];
            this.state = {
                isRecoverPassword: true, recoveryUserCode: userCode
            };
        } else {
            this.state = {
                isLogOn: isActiveUser
            };
        }
        this.onLogInSucess = this.onLogInSucess.bind(this);
    }

    render() {
        return (
            <div>
                <ToastContainer
                    position="top-center"
                    transition={Slide}
                />
                {this.state.isRecoverPassword ?
                    <RecoverPassword userCode={this.state.recoveryUserCode}/> : (this.state.isLogOn ?
                    <Main /> :
                    <LogIn onLogInSucess={() => { this.onLogInSucess() }} />)
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
