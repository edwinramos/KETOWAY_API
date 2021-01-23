import React, { Component } from "react";
import Alert from 'react-bootstrap/Alert';
import Cookies from 'js-cookie';

export default class LogIn extends Component {
    state = {
        userCode: "", password: "", showAlert: false, errorMessage: ""
    };
    constructor(props) {
        super(props);
        this.setShow = this.setShow.bind(this);
        this.onChange = this.onChange.bind(this);
        this.onLoginHandler = this.onLoginHandler.bind(this);
    }
    render() {
        const { onLogInSucess } = this.props
        return (
            <div>
                <Alert variant="danger" show={this.state.showAlert} onClose={() => this.setShow(false)} dismissible style={{
                    position: 'absolute', left: '50%', top: '50%',
                    transform: 'translate(-50%, -270%)', width: "500px"
                }}>
                    <Alert.Heading>Error!</Alert.Heading>
                    <p>
                        {this.state.errorMessage}
                    </p>
                </Alert>
                <form onSubmit={(e) => { e.preventDefault(); this.onLoginHandler(onLogInSucess); }} style={{
                    position: 'absolute', left: '50%', top: '50%',
                    transform: 'translate(-50%, -50%)', width: "500px"
                }}>
                    <h3>Sign In</h3>

                    <div className="form-group">
                        <label>Usuario</label>
                        <input name="userCode" className="form-control" placeholder="Enter user name" onChange={this.onChange} />
                    </div>

                    <div className="form-group">
                        <label>Contraseña</label>
                        <input type="password" name="password" className="form-control" placeholder="Enter password" onChange={this.onChange} />
                    </div>

                    <div className="form-group">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id="customCheck1" />
                            <label className="custom-control-label" htmlFor="customCheck1">Remember me</label>
                        </div>
                    </div>

                    <button type="submit" className="btn btn-primary btn-block">Log In</button>
                    <p className="forgot-password text-right">
                        Forgot <a href="#">password?</a>
                    </p>
                </form>
            </div>
        );
    }
    setShow(flag, msg) {
        this.setState({ showAlert: flag, errorMessage: msg });
    }

    onLoginHandler(onLogInSucess) {
        const that = this;
        var url = 'api/User/userLogIn';
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify({ "userCode": this.state.userCode, "password": this.state.password })
        };
        fetch(url, requestOptions)
            .then(function (response) {
                //console.log(response)
                return response.json();
            })
            .then(function (myJson) {
                if (myJson.success) {
                    Cookies.set('activeUser', that.state.userCode, { expires: 1 });
                    onLogInSucess();
                }
                else
                    that.setShow(true, myJson.message);
            });
    }
    onChange(event) {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }
}