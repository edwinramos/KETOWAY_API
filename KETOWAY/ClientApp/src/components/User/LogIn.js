import React, { Component } from "react";
import { showToast, setCookie, toastType, PostData } from "../Helper";

export default class LogIn extends Component {
    state = {
        userCode: "", password: ""
    };
    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
        this.onLoginHandler = this.onLoginHandler.bind(this);
    }
    render() {
        const { onLogInSucess } = this.props
        return (
            <div>
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

    async onLoginHandler(onLogInSucess) {
        var url = 'api/User/userLogIn';
        var body = { "userCode": this.state.userCode, "password": this.state.password };
        var apiRequest = { body: body }
        var result = await PostData(url, apiRequest);
        if (result) {
            setCookie('activeUser', this.state.userCode);
            showToast("Welcome " + result.name + "!", toastType.SUCCESS)
            onLogInSucess();
        }
    }
    onChange(event) {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }
}