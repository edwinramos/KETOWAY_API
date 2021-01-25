import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import Icon from "@material-ui/core/Icon";
import Button from 'react-bootstrap/Button';
import BootstrapTable from 'react-bootstrap/Table'
import EditModal from './UserModal';
import { showToast, toastType, PostData, GetData, DeleteData } from "../Helper";

export class User extends Component {
    static displayName = User.name;

    constructor(props) {
        super(props);
        this.state = { users: [], objectEditing: false, objectToEdit: {} };

        this.openEditModal = this.openEditModal.bind(this)
        this.closeEditModal = this.closeEditModal.bind(this)
        this.updateData = this.updateData.bind(this)
        this.deleteUser = this.deleteUser.bind(this)
    }

    componentDidMount() {
        this.getUsers();
    }

    render() {
        return (
            <div>
                <Card style={{ width: '100%' }}>
                    <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                        <div style={{ float: 'left' }}>
                            <h4>Usuarios</h4>
                            <p>
                                Lista de Usuarios
                        </p>
                            <Button color="info" style={{ float: 'right' }} onClick={() => this.openEditModal("0")}><Icon>add_circle_outline</Icon> Agregar</Button>
                        </div>
                    </Card.Header>
                    <Card.Body>
                        <BootstrapTable responsive>
                            <thead>
                                <tr>
                                    <th>Usuario</th>
                                    <th>Genero</th>
                                    <th>Nombre</th>
                                    <th>Pais</th>
                                    <th>Imagen</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.users.map(user =>
                                    <tr key={user.userCode}>
                                        <td>{user.userCode}</td>
                                        <td>{user.gender}</td>
                                        <td>{user.name} {user.lastName}</td>
                                        <td>{user.countryCode}</td>
                                        <td>{user.image}</td>
                                        <td><Button onClick={() => this.openEditModal(user.userCode)} color="info">Editar</Button> <Button onClick={() => this.deleteUser(user.userCode)} color="danger">Eliminar</Button></td>
                                    </tr>
                                )}
                            </tbody>
                        </BootstrapTable>
                    </Card.Body>
                </Card>
                <EditModal onUpdate={this.updateData} onClose={this.closeEditModal} isOpen={this.state.objectEditing} objectToEdit={this.state.objectToEdit} />
            </div>
        );
    }

    async getUsers() {
        var data = await GetData("api/User");
        this.setState({ users: data, loading: false });
    }

    async openEditModal(userCode) {
        var result = await GetData("api/User/getUser/" + userCode);
        if (result)
            this.setState({ objectToEdit: result, objectEditing: true });
    }
    closeEditModal() {
        this.setState({ objectEditing: false })
    }

    async updateData(obj) {
        var url = 'api/User/postUser';
        var apiRequest = { Body: obj };
        var result = await PostData(url, apiRequest);
        if (result) {
            this.getUsers();
            this.setState({ objectToEdit: {}, objectEditing: false });
        }
    }

    async deleteUser(code) {
        await DeleteData("api/User/" + code,);
        this.getUsers();
    }

}
