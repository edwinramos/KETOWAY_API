import React, { Component } from "react";
import { showToast, setCookie, toastType, PostData } from "../Helper";

export default class RecoverPassword extends Component {
    state = {
        userCode: "", password: "", confirmPassword: "", success: false
    };
    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
        this.onLoginHandler = this.onLoginHandler.bind(this);
        this.closeEditModal = this.closeEditModal.bind(this);
    }
    render() {
        const { userCode } = this.props // destructure
        //this.setState({ userCode: userCode });
        return (
            <div>
                {
                    this.state.success ?
                        <div>
                            <h1>Password changed successfully!</h1>
                            <p>Now try to log in with your new password anytime!</p>
                        </div>
                        : <form onSubmit={(e) => { e.preventDefault(); this.onLoginHandler(userCode); }} style={{
                            position: 'absolute', left: '50%', top: '50%',
                            transform: 'translate(-50%, -50%)', width: "500px"
                        }}>
                            <h3>Recover Password</h3>

                            <div className="form-group">
                                <label>Nueva Contraseña</label>
                                <input type="password" name="password" className="form-control" placeholder="Enter password" onChange={this.onChange} />
                            </div>

                            <div className="form-group">
                                <label>Confirmar Contraseña</label>
                                <input type="password" name="confirmPassword" className="form-control" placeholder="Confirm password" onChange={this.onChange} />
                            </div>

                            <button type="submit" className="btn btn-primary btn-block">Cambiar Contraseña</button>
                        </form>
                }
            </div>
        );
    }

    async onLoginHandler(userCode) {
        if (this.state.password === this.state.confirmPassword) {
            var url = 'api/User/recoverPassword';
            var body = { "userCode": userCode, "password": this.state.password };
            var apiRequest = { payload: body }
            var result = await PostData(url, apiRequest);
            if (result) {
                this.setState({ success: true });
            }
        } else {
            showToast("Passwords not matching, please try again.", toastType.ERROR);
        }
    }
    onChange(event) {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }
    closeEditModal() {
        this.setState({ hasForgotPassword: false })
    }
}