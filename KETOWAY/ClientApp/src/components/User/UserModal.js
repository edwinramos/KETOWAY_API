import React, { Component } from 'react';
// core components
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Icon from "@material-ui/core/Icon";
import Modal from 'react-bootstrap/Modal';
import InputGroup from 'react-bootstrap/InputGroup'
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import { PostData, GetData, DeleteData } from "../Helper";

class UserModal extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        countries: [], states: []
    };
    constructor(props) {
        super(props);

        var url = 'api/MobileApi/getFoodGroups';
        var result = GetData(url);
        if (result) {
            this.setState({ foodGroups: result });
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.onFileChange = this.onFileChange.bind(this);
    }
    render() {
        const { onUpdate, onClose, isOpen, objectToEdit } = this.props // destructure
        //this.setState({ currentFoodCode: dataList[0].foodCode});
        var m = 0;
        return (
            <Modal
                size="lg"
                show={isOpen}
                onHide={() => onClose()}
                aria-labelledby="example-modal-sizes-title-lg">
                <Modal.Header closeButton>
                    <Modal.Title id="example-modal-sizes-title-lg">
                        Editar Usuario
            </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={this.handleSubmit(onUpdate)}>
                        <Container>
                            <Row md={12}>
                                <Col md={6}>
                                    <Form.Group controlId="UserCode">
                                        <Form.Label>Nombre de Usuario</Form.Label>
                                        <InputGroup>
                                            <InputGroup.Prepend>
                                                <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
                                            </InputGroup.Prepend>
                                            <Form.Control defaultValue={objectToEdit.userCode} aria-describedby="inputGroupPrepend" required />
                                        </InputGroup>
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="Password">
                                        <Form.Label>Contraseña</Form.Label>
                                        <Form.Control type="password" defaultValue={objectToEdit.password} required />
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="Name">
                                        <Form.Label>Nombre</Form.Label>
                                        <Form.Control defaultValue={objectToEdit.name} />
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="LastName">
                                        <Form.Label>Apellidos</Form.Label>
                                        <Form.Control defaultValue={objectToEdit.lastName} />
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="Gender">
                                        <Form.Label>Genero</Form.Label>
                                        <Form.Control as="select" defaulValue={objectToEdit.gender}>
                                            <option value={"H"}>Hombre</option>
                                            <option value={"M"}>Mujer</option>
                                        </Form.Control>
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="BirthDate">
                                        <Form.Label>Fecha de Nacimiento</Form.Label>
                                        <Form.Control type="datetime-local" defaultValue={objectToEdit.birthDate} required />
                                    </Form.Group>
                                </Col>
                                <Col md={6}>
                                    <Form.Group controlId="Email">
                                        <Form.Label>Email</Form.Label>
                                        <Form.Control type="email" placeholder="name@example.com" defaultValue={objectToEdit.email} />
                                    </Form.Group>
                                </Col>
                                {
                                    (objectToEdit.userCode === "") ?
                                        <br /> :
                                        <Col md={6}>
                                            <Form.Group controlId="ImagePath">
                                                <Form.Label>Imagen</Form.Label>
                                                <Form.File defaultValue={objectToEdit.imagePath} onChange={this.onFileChange(objectToEdit.userCode)}
                                                    id="custom-file"
                                                    label="Custom file input"
                                                    custom
                                                />
                                            </Form.Group>
                                        </Col>
                                }
                            </Row>
                        </Container>
                        <Button type="submit">Guardar</Button>
                    </Form>
                </Modal.Body>
            </Modal>
        );
    }

    handleSubmit(onUpdate) {
        return e => {
            e.preventDefault();

            const formData = new FormData(e.target);
            console.log(formData);
            console.log(formData.entries());
            var formDataObj = Object.fromEntries(formData.entries());
            console.log(formDataObj);
            var obj =
            {
                UserCode: e.currentTarget[0].value,
                Password: e.currentTarget[1].value,
                Name: e.currentTarget[2].value,
                LastName: e.currentTarget[3].value,
                Gender: e.currentTarget[4].value,
                BirthDate: new Date(e.currentTarget[5].value).toUTCString(),
                Email: e.currentTarget[6].value,
                ImagePath: e.currentTarget[7].value
            };

            onUpdate(obj);
        }
    }

    onFileChange(code) {
        return async e => {
            e.preventDefault();
            var obj = e.target.files[0];
            var url = 'api/User/userImage';
            var apiRequest = { payload: obj };
            var result = await PostData(url, apiRequest);
            if (result) {
                //this.getUsers();
            } else
                console.log(result);
        }
    }
}
export default UserModal;