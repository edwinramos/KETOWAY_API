import React, { Component } from 'react';
// core components
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import { PostData, showToast, toastType } from "../Helper";

class ForgotPasswordModal extends Component {
    state = {
        mailSent: false
    };
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);
    }
    render() {
        const { onClose, isOpen } = this.props // destructure
        return (
            <Modal
                size="md"
                centered
                show={isOpen}
                onHide={() => onClose()}
                aria-labelledby="example-modal-sizes-title-lg">
                <Modal.Header closeButton>
                    <Modal.Title id="example-modal-sizes-title-lg">
                        Recuperar contraseña
            </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {this.state.mailSent ?
                        <div>
                            <h1>Correo endiado</h1>
                            <p>Por favor verificar su correo para cmabiar su contraseña.</p>
                        </div> :
                        <Form onSubmit={this.handleSubmit()}>
                            <Container>
                                <Row md={12}>
                                    <Col md={12}>
                                        <Form.Group controlId="Email">
                                            <Form.Label>Inserte su Email</Form.Label>
                                            <Form.Control type="email" placeholder="name@example.com" />
                                        </Form.Group>
                                    </Col>
                                    <Col md={12}>
                                        <Form.Group controlId="Email">
                                            <Form.Label>Confirme su Email</Form.Label>
                                            <Form.Control type="email" placeholder="name@example.com" />
                                        </Form.Group>
                                    </Col>
                                </Row>
                            </Container>
                            <Button type="submit">Guardar</Button>
                        </Form>
                    }
                </Modal.Body>
            </Modal>
        );
    }

    handleSubmit() {
        return async e => {
            e.preventDefault();

            var email = e.currentTarget[0].value;
            var email2 = e.currentTarget[1].value;
            if (email === email2) {
                var apiRequest = { payload: email };
                var result = await PostData("api/User/forgotPassword", apiRequest);
                if (result)
                    this.setState({ mailSent: true });
            } else {
                showToast("Emails not matching, please try again.", toastType.ERROR);
            }
        }
    }


}
export default ForgotPasswordModal;