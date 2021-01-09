import React, { Component } from "react";
import { useHistory } from "react-router-dom";

export default class LogIn extends Component {
    
    render() {
        const { onLogIn } = this.props 
        return (
            <form onSubmit={() => onLogIn()} style={{
                position: 'absolute', left: '50%', top: '50%',
                transform: 'translate(-50%, -50%)', width: "500px"
            }}>
                <h3>Sign In</h3>

                <div className="form-group">
                    <label>Email address</label>
                    <input type="email" className="form-control" placeholder="Enter email" />
                </div>

                <div className="form-group">
                    <label>Password</label>
                    <input type="password" className="form-control" placeholder="Enter password" />
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
        );
    }
}