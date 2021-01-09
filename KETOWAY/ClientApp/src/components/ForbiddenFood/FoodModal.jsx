import React, { Component } from 'react';
// core components
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Icon from "@material-ui/core/Icon";
import Modal from 'react-bootstrap/Modal';
import JoditEditor from "jodit-react";
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import ReactDOM from 'react-dom';
class FoodModal extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        recipes: [], recipesToPost: [], foodGroups: []
    };
    constructor(props) {
        super(props);

        var url = 'api/MobileApi/getFoodGroups';
        fetch(url)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ foodGroups: result });
                },
                (error) => {
                    console.log(error);
                }
            );

        this.onSubmit = this.onSubmit.bind(this);
        this.handleRecipes = this.handleRecipes.bind(this);
    }
    render() {
        const { onUpdate, onClose, isOpen, dataList } = this.props // destructure
        //this.setState({ currentFoodCode: dataList[0].foodCode});

        return (
            <Container>
                <Row xs={12} sm={12} md={12}>
                    <Modal
                        size="lg"
                        show={isOpen}
                        onHide={() => onClose()}
                        aria-labelledby="example-modal-sizes-title-lg">
                        <Modal.Header closeButton>
                            <Modal.Title id="example-modal-sizes-title-lg">
                                Editar Alimento
            </Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Accordion>
                                {dataList.map((item) => {

                                    var editor = <JoditEditor key={"b"} value={item.foodContent} onChange={(newContent) => this.handleRecipes(item.foodCode, item.langCode, item.foodTitle, newContent)} />;

                                    return <Card>
                                        <Card.Header>
                                            <Accordion.Toggle as={Button} variant="link" eventKey={item.langCode}>
                                                {item.foodTitle} ({item.langCode})
                          </Accordion.Toggle>
                                        </Card.Header>
                                        <Accordion.Collapse eventKey={item.langCode}>
                                            <Card.Body style={{ overflowY: 'auto' }}>

                                                <Form ref={'frm_' + item.langCode}>
                                                    <Form.Group controlId="LangCode">
                                                        <Form.Control type="hidden" defaultValue={item.langCode} />
                                                    </Form.Group>
                                                    <Form.Group controlId="FoodCode">
                                                        <Form.Control type="hidden" defaultValue={item.foodCode} />
                                                    </Form.Group>
                                                    <Form.Group controlId="FoodTitle">
                                                        <Form.Label>Titulo</Form.Label>
                                                        <Form.Control type="text" defaultValue={item.foodTitle} />
                                                    </Form.Group>
                                                    <Form.Group controlId="FoodGroupID">
                                                        <Form.Label>Grupo Alimenticio</Form.Label>
                                                        <Form.Control as="select" defaultValue={item.foodGroupID}>
                                                            {this.state.foodGroups.filter(x => x.langCode == item.langCode).map((fGroup) => {
                                                                return <option value={fGroup.id} >{fGroup.foodGroupDescription}</option>
                                                            })}
                                                        </Form.Control>
                                                    </Form.Group>
                                                    <Form.Group controlId="FoodContent">
                                                        <Form.Label>Contenido</Form.Label>
                                                        {editor}
                                                    </Form.Group>
                                                </Form>
                                            </Card.Body>
                                        </Accordion.Collapse>
                                    </Card>
                                })}
                            </Accordion>
                            <Button onClick={() => { this.onSubmit(onUpdate); }} style={{ float: 'right' }} color="info"><Icon>publish</Icon> Publicar</Button>
                        </Modal.Body>
                    </Modal>
                </Row>
            </Container>
        );
    }

    onSubmit(onUpdate) {
        //this.state.currentFoodCode;
        const arr = this.state.recipesToPost;
        Object.keys(this.refs)
            .filter(key => key.substr(0, 3) === 'frm')
            .forEach(key => {
                var obj = {
                    FoodCode: ReactDOM.findDOMNode(this.refs[key]).elements.FoodCode.value,
                    FoodTitle: ReactDOM.findDOMNode(this.refs[key]).elements.FoodTitle.value,
                    FoodGroupID: Number(ReactDOM.findDOMNode(this.refs[key]).elements.FoodGroupID.value),
                    FoodContent: "",//ReactDOM.findDOMNode(this.refs[key]).elements.FoodContent.value, 
                    LangCode: ReactDOM.findDOMNode(this.refs[key]).elements.LangCode.value,
                    IsAllowed: false
                };

                var m = arr.find(x => x.LangCode === obj.LangCode);
                if (!m) {
                    arr.push(obj);
                }
                else {
                    obj.FoodContent = m.FoodContent;
                    var index = arr.findIndex(x => x.LangCode === obj.LangCode);
                    arr[index] = obj;
                }

            });
        //var m = this.refs;//["frm_es"].elements.FoodTitle.value;
        this.setState({ recipesToPost: arr });
        onUpdate(this.state.recipesToPost);
    }

    handleRecipes(code, langCode, title, value) {
        var arr = this.state.recipesToPost;
        var obj = { FoodCode: code, FoodTitle: title, FoodGroupID: 0, FoodContent: value, LangCode: langCode, IsAllowed: false };
        var m = arr.find(x => x.LangCode === langCode);
        if (!m) {
            obj.FoodCode = code;
            obj.FoodTitle = title;
            obj.FoodContent = value;
            obj.LangCode = langCode;
            arr.push(obj);
        }
        else {
            obj.FoodTitle = m.FoodTitle;
            obj.FoodGroupID = m.FoodGroupID;
            var index = arr.findIndex(x => x.LangCode === langCode);
            arr[index] = obj;
        }

        this.setState({ recipesToPost: arr });
    }
}
export default FoodModal;