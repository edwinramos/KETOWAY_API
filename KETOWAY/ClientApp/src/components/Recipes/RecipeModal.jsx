import React, { Component } from 'react';
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

class RecipeModal extends Component {
    state = { classes:{cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251"},
            recipes: [], recipesToPost: [], titleEs:"", titleEn: ""
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
                <Row xs={12} sm={12} md={12}>
                    <Modal
                        size="lg"
                        show={isOpen}
                        onHide={() => onClose()}
                        aria-labelledby="example-modal-sizes-title-lg">
                        <Modal.Header closeButton>
                            <Modal.Title id="example-modal-sizes-title-lg">
                                Editar Receta
            </Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Accordion>
                                {dataList.map((item) => {

                                    var editor = <JoditEditor key={"b"} value={item.recipeContent} onChange={(newContent) => this.handleRecipes(item.recipeCode, item.langCode, item.recipeTitle, newContent)} />;

                                    return <Card>
                                        <Card.Header>
                                            <Accordion.Toggle as={Button} variant="link" eventKey={item.langCode}>
                                                {item.recipeTitle} ({item.langCode})
                          </Accordion.Toggle>
                                        </Card.Header>
                                        <Accordion.Collapse eventKey={item.langCode}>
                                            <Card.Body style={{ overflowY: 'auto' }}>

                                                <Form ref={'frm_' + item.langCode}>
                                                    <Form.Group controlId="LangCode">
                                                        <Form.Control type="hidden" defaultValue={item.langCode} />
                                                    </Form.Group>
                                                    <Form.Group controlId="RecipeCode">
                                                        <Form.Control type="hidden" defaultValue={item.recipeCode} />
                                                    </Form.Group>
                                                    <Form.Group controlId="RecipeTitle">
                                                        <Form.Label>Titulo</Form.Label>
                                                        <Form.Control type="text" defaultValue={item.recipeTitle} />
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
                    RecipeCode: ReactDOM.findDOMNode(this.refs[key]).elements.RecipeCode.value,
                    RecipeTitle: ReactDOM.findDOMNode(this.refs[key]).elements.RecipeTitle.value,
                    RecipeContent: "",//ReactDOM.findDOMNode(this.refs[key]).elements.FoodContent.value, 
                    LangCode: ReactDOM.findDOMNode(this.refs[key]).elements.LangCode.value
                };

                var m = arr.find(x => x.LangCode === obj.LangCode);
                if (!m) {
                    arr.push(obj);
                }
                else {
                    obj.RecipeContent = m.RecipeContent;
                    var index = arr.findIndex(x => x.LangCode === obj.LangCode);
                    arr[index] = obj;
                }

            });
        //var m = this.refs;//["frm_es"].elements.FoodTitle.value;
        this.setState({ recipesToPost: arr });
        onUpdate(this.state.recipesToPost);
    }

    handleRecipes(recipeCode, langCode, title, value){
      var arr = this.state.recipesToPost;
        var obj = {RecipeCode: recipeCode, RecipeTitle:title, RecipeContent: value, LangCode:langCode};
        var m = arr.find(x => x.LangCode === langCode);
        if(!m)
            arr.push(obj);
        else{
          obj.RecipeTitle = m.RecipeTitle;
            var index = arr.findIndex(x => x.LangCode === langCode);
            arr[index] = obj;
        }        
        
        this.setState({recipesToPost:arr});
    }
    handleTitleChange(recipeCode, langCode, recipeTitle, content){
      var arr = this.state.recipesToPost;
      var m = arr.find(x => x.LangCode === langCode);
      var obj = {RecipeCode: recipeCode, RecipeTitle:recipeTitle, RecipeContent: content, LangCode:langCode};
      if(!m)
          arr.push(obj);
      else{
          obj.RecipeContent = m.RecipeContent;
          var index = arr.findIndex(x => x.LangCode === langCode);
          arr[index] = obj;
      }

      this.setState({recipesToPost:arr});
    }
}
export default RecipeModal;