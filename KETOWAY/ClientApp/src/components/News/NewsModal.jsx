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

class NewsModal extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        recipes: [], recipesToPost: [], titleEs: "", titleEn: ""
    };
    constructor(props) {
        super(props);
        this.handleRecipes = this.handleRecipes.bind(this);
        this.handleTitleChange = this.handleTitleChange.bind(this);
    }
    render() {
        const { onUpdate, onClose, isOpen, dataList } = this.props // destructure

        return (
            <Container>
                <Modal
                    size="xl"
                    show={isOpen}
                    onHide={() => onClose()}
                    aria-labelledby="example-modal-sizes-title-lg">
                    <Modal.Header closeButton>
                        <Modal.Title id="example-modal-sizes-title-lg">
                            Editar Noticia
            </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Accordion>
                            {dataList.map((item) => {

                                var editor = <JoditEditor key={"b"} value={item.newsContent} onChange={(newContent) => this.handleRecipes(item.newsCode, item.langCode, item.newsTitle, newContent)} />;
                                if (item.langCode === "es") {
                                    editor = <JoditEditor key={"b"} value={item.newsContent} onChange={(newContent) => this.handleRecipes(item.newsCode, item.langCode, item.newsTitle, newContent)} />;
                                    //this.setState({titleEs: item.Title});
                                }
                                else {
                                    editor = <JoditEditor key={"b"} value={item.newsContent} onChange={(newContent) => this.handleRecipes(item.newsCode, item.langCode, item.newsTitle, newContent)} />
                                    //this.setState({titleEn: item.Title});
                                }
                                return <Card>
                                    <Card.Header>
                                        <Accordion.Toggle as={Button} variant="link" eventKey={item.langCode}>
                                            {item.newsTitle} ({item.langCode})
                          </Accordion.Toggle>
                                    </Card.Header>
                                    <Accordion.Collapse eventKey={item.langCode}>
                                        <Form ref={'frm_' + item.langCode}>
                                            <Form.Group controlId="LangCode">
                                                <Form.Control type="hidden" defaultValue={item.langCode} />
                                            </Form.Group>
                                            <Form.Group controlId="NewsCode">
                                                <Form.Control type="hidden" defaultValue={item.newsCode} />
                                            </Form.Group>
                                            <Form.Group controlId="NewsTitle">
                                                <Form.Label>Titulo</Form.Label>
                                                <Form.Control type="text" defaultValue={item.newsTitle} onChange={(e) => { this.handleTitleChange(item.newsCode, item.langCode, e.target.value, item.newsContent) }}/>
                                            </Form.Group>
                                            <Form.Group controlId="FoodContent">
                                                <Form.Label>Contenido</Form.Label>
                                                {editor}
                                            </Form.Group>
                                        </Form>
                                    </Accordion.Collapse>
                                </Card>
                            })}
                        </Accordion>
                        <Button onClick={() => { onUpdate(this.state.recipesToPost); }} style={{ float: 'right' }} color="info"><Icon>publish</Icon> Publicar</Button>
                    </Modal.Body>
                </Modal>
            </Container>
        );
    }

    handleRecipes(code, langCode, title, value) {
        var arr = this.state.recipesToPost;
        var obj = { NewsCode: code, NewsTitle: title, NewsContent: value, LangCode: langCode };
        var m = arr.find(x => x.LangCode === langCode);
        if (!m)
            arr.push(obj);
        else {
            obj.NewsTitle = m.NewsTitle;
            var index = arr.findIndex(x => x.LangCode === langCode);
            arr[index] = obj;
        }

        this.setState({ recipesToPost: arr });
    }
    handleTitleChange(code, langCode, title, content) {
        var arr = this.state.recipesToPost;
        var m = arr.find(x => x.LangCode === langCode);
        var obj = { NewsCode: code, NewsTitle: title, NewsContent: content, LangCode: langCode };
        if (!m)
            arr.push(obj);
        else {
            obj.NewsContent = m.NewsContent;
            var index = arr.findIndex(x => x.LangCode === langCode);
            arr[index] = obj;
        }

        this.setState({ recipesToPost: arr });
    }
}
export default NewsModal;