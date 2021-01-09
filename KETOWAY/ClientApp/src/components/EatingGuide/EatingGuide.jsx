import React, { Component } from 'react';
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Icon from "@material-ui/core/Icon";
import BootstrapTable from 'react-bootstrap/Table'
import ReactDOM from 'react-dom';
import Form from 'react-bootstrap/Form'
import JoditEditor from "jodit-react";
import DetailModal from './EatingGuideDetailModal';

class EatingGuide extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        dataList: [], items: [], isDetailOpen: false, langCode: "es"
    };

    constructor() {
        super();
        fetch("api/MobileApi/getEatingGuide")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ dataList: result });
                },
                (error) => { console.log(error); }
            );

        this.onItemsRefresh = this.onItemsRefresh.bind(this);
        this.updateDataList = this.updateDataList.bind(this);
        this.postData = this.postData.bind(this);
        this.openEditModal = this.openEditModal.bind(this);
        this.closeEditModal = this.closeEditModal.bind(this);
    }


    render() {
        return (
            <div>
                <Card style={{ width: '100%' }}>
                    <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                        <div style={{ float: 'left' }}>
                            <h4>Guias Alimenticias</h4>
                            <p>Lista de Guias Alimenticias para la app.</p>
                            <Button color="info" style={{ float: 'right' }} onClick={this.postData}><Icon>publish</Icon> Publicar</Button>
                        </div>
                    </Card.Header>
                    <Card.Body>
                        {this.state.dataList.map((item) => {
                            return <Accordion defaultActiveKey="0">
                                <Card>
                                    <Card.Header>
                                        <Accordion.Toggle as={Button} variant="link" eventKey={item.langCode}>
                                            {item.eatingGuideTitle} ({item.langCode})
      </Accordion.Toggle>
                                    </Card.Header>
                                    <Accordion.Collapse eventKey={item.langCode}>
                                        <Card.Body>
                                            <Button onClick={() => this.openEditModal(item.id, item.langCode)} color="info"><Icon>kitchen </Icon> Ver Articulos</Button>
                                            <Form ref={'frm_' + item.langCode}>
                                                <Form.Group controlId="LangCode">
                                                    <Form.Control type="hidden" defaultValue={item.langCode} />
                                                </Form.Group>
                                                <Form.Group controlId="ID">
                                                    <Form.Control type="hidden" defaultValue={item.id} />
                                                </Form.Group>
                                                <Form.Group controlId="EatingGuideTitle">
                                                    <Form.Label>Titulo</Form.Label>
                                                    <Form.Control type="text" defaultValue={item.eatingGuideTitle} />
                                                </Form.Group>
                                                <Form.Group controlId="FoodContent">
                                                    <Form.Label>Contenido</Form.Label>
                                                    <JoditEditor value={item.eatingGuideContent} onChange={(newContent) => this.updateDataList(item.id, item.langCode, newContent)} />
                                                </Form.Group>
                                            </Form>

                                            <DetailModal onClose={this.closeEditModal} onItemsRefresh={this.onItemsRefresh} langCode={this.state.langCode} items={this.state.items} isOpen={this.state.isDetailOpen} />
                                        </Card.Body>
                                    </Accordion.Collapse>
                                </Card>
                            </Accordion>
                        })}
                    </Card.Body>
                </Card>
            </div>
        );
    }
    updateDataList(id, langCode, content) {
        var arr = this.state.dataList;
        var obj = { id: id, langCode: langCode, eatingGuideContent: content };
        var m = arr.find(x => x.id === id && x.langCode === langCode);
        if (!m)
            arr.push(obj);
        else {
            obj.RecipeTitle = m.RecipeTitle;
            var index = arr.findIndex(x => x.id === id && x.langCode === langCode);
            arr[index] = obj;
        }
        this.setState({ dataList: arr });
    }

    postData() {
        const arr = this.state.dataList;
        Object.keys(this.refs)
            .filter(key => key.substr(0, 3) === 'frm')
            .forEach(key => {
                var obj = {
                    id: Number(ReactDOM.findDOMNode(this.refs[key]).elements.ID.value),
                    eatingGuideTitle: ReactDOM.findDOMNode(this.refs[key]).elements.EatingGuideTitle.value,
                    eatingGuideContent: "",//Number(ReactDOM.findDOMNode(this.refs[key]).elements.EatingGuideContent.value), 
                    langCode: ReactDOM.findDOMNode(this.refs[key]).elements.LangCode.value
                };

                var m = arr.find(x => x.id === obj.id && x.langCode === obj.langCode);
                if (!m) {
                    arr.push(obj);
                }
                else {
                    obj.eatingGuideContent = m.eatingGuideContent;
                    var index = arr.findIndex(x => x.langCode === obj.langCode);
                    arr[index] = obj;
                }

            });
        var url = 'api/MobileApi/postEatingGuide';
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(arr)
        };
        fetch(url, requestOptions)
            .then(
                (result) => {
                    // console.log(result);
                },
                (error) => {
                    console.log(error);
                });
    }

    onItemsRefresh() {
        var url = 'api/MobileApi/getEatingGuideDetail/' + 1 + '?langCode=es';
        fetch(url)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ items: result });
                    // this.setState({ items : result});
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    openEditModal(id, langCode) {
        this.setState({ langCode: langCode });
        var url = 'api/MobileApi/getEatingGuideDetail/' + id + '?langCode=' + langCode;
        fetch(url)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ isDetailOpen: true, items: result });
                    fetch("api/MobileApi/getFoods/" + true)
                        .then(res => res.json())
                        .then(
                            (result) => {
                                this.setState({ foodList: result });
                            },
                            (error) => {
                                console.log(error);
                            }
                        );
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    closeEditModal() {
        this.setState({ isDetailOpen: false, items: [] })
    }
}
export default EatingGuide;