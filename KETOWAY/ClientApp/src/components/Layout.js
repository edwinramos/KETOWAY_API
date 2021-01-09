import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import Sidebar from "react-sidebar";
import routes from './routes';
import { Link } from 'react-router-dom';
export class Layout extends Component {
    
    static displayName = Layout.name;
    constructor(props) {
        super(props);
        this.state = {
            sidebarOpen: false
        };
        this.onSetSidebarOpen = this.onSetSidebarOpen.bind(this);
    }

    onSetSidebarOpen(open) {
        this.setState({ sidebarOpen: open });
    }

    render() {
        return (
            <div>
                <NavMenu />
                <Sidebar
                    sidebar={<ul>
                        <NavItem>
                        {routes.map((prop, key) => <NavLink tag={Link} className="text-dark" to={prop.path}>{prop.name}</NavLink>)}
                            </NavItem>
                    </ul>}
                    open={this.state.sidebarOpen}
                    onSetOpen={this.onSetSidebarOpen}
                    styles={{ sidebar: { background: "white" } }}
                >
                    <button onClick={() => this.onSetSidebarOpen(true)}>
                        Open sidebar
        </button>
                </Sidebar>
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
