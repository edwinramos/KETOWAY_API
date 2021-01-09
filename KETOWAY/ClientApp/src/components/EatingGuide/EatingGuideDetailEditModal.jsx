import React, { Component } from 'react';
// core components
import Button from 'react-bootstrap/Button';
import Icon from "@material-ui/core/Icon";
import Modal from 'react-bootstrap/Modal';
import { Col, Row, Form } from "react-bootstrap";
import ReactDOM from 'react-dom';

class EatingGuideDetailEditModal extends Component {
    state = { classes:{cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251"},
            items:[], foodList: [], sections: [], measurementUnits: []
          };
          constructor(props) {
            super(props);
            fetch("api/MobileApi/getEatingGuideDetailData?langCode="+this.props.langCode)
            .then(res => res.json())
            .then(
                (result) => {
                  this.setState({foodList: result.foodList, sections: result.sections, measurementUnits: result.measurementUnits});
                  // ReactDOM.findDOMNode(this.refs["frm_edit"]).elements.Quantitu.value = 7;
                },
                (error) => {
                    console.log(error);
                }
            );
            this.onSubmit = this.onSubmit.bind(this);
          }
    render() {
       const { onClose, objEdit, langCode, isOpen } = this.props // destructure

        return (
            <Modal
          size="lg"
          show={isOpen}
          onHide={() => onClose()}
          aria-labelledby="example-modal-sizes-title-lg">
          <Modal.Header closeButton>
            <Modal.Title id="example-modal-sizes-title-lg">
            Editar Guia
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
                  <Form ref={'frm_edit'}>
                    <Form.Group controlId="HeadID">
                      <Form.Control type="hidden" defaultValue={objEdit.headId} />
                    </Form.Group>
                    <Form.Row>
                      <Col>
                        <Form.Group controlId="FoodCode">
                          <Form.Label>Alimento</Form.Label>
                          <Form.Control as="select" defaultValue={objEdit.foodCode}>
                          {this.state.foodList.map((itm) => 
                                                      { 
                                                        return <option value={itm.foodCode} >{itm.foodTitle}</option> 
                                                        })}
                          </Form.Control>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group controlId="SectionID">
                          <Form.Label>Seccion</Form.Label>
                          <Form.Control as="select" defaultValue={objEdit.sectionId}>
                          {this.state.sections.map((itm) => 
                                                      { 
                                                        return <option value={itm.id} >{itm.sectionDescription}</option> 
                                                        })}
                          </Form.Control>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group controlId="Fat">
                          <Form.Label>Grasa</Form.Label>
                          <Form.Control type="number" defaultValue={objEdit.fat}/>
                        </Form.Group>
                      </Col>
                    </Form.Row>
                    <Form.Row>
                      <Col>
                        <Form.Group controlId="Calories">
                          <Form.Label>Calorias</Form.Label>
                          <Form.Control type="number" defaultValue={objEdit.calories}/>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group controlId="Carbs">
                          <Form.Label>Carbohidratos</Form.Label>
                          <Form.Control type="number" defaultValue={objEdit.carbs}/>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group controlId="Protein">
                          <Form.Label>Proteinas</Form.Label>
                          <Form.Control type="number" defaultValue={objEdit.protein}/>
                        </Form.Group>
                      </Col>
                    </Form.Row>
                    <Form.Row>
                    <Col>
                        <Form.Group controlId="QuntityMeasurementUnit">
                          <Form.Label>Unidad de Medida para Cantidad</Form.Label>
                          <Form.Control as="select" defaultValue={objEdit.quantity_measurementUnitCode}>
                          {this.state.measurementUnits.map((itm) => 
                                                      { 
                                                        return <option value={itm.code} >{itm.description}</option> 
                                                        })}
                          </Form.Control>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group controlId="Quantity">
                          <Form.Label>Quantity</Form.Label>
                          <Form.Control type="number" defaultValue={objEdit.quantity}/>
                        </Form.Group>
                      </Col>
                      <Col>
                        <Form.Group>
                          <Button onClick={()=> { this.onSubmit(onClose);} } color="success"><Icon>save</Icon>Guardar</Button>
                        </Form.Group>
                      </Col>
                    </Form.Row>
                  </Form>
          </Modal.Body>
        </Modal>
            );
    }

    onSubmit(onClose){
      var obj = {};
      Object.keys(this.refs)
    .filter(key => key.substr(0,3) === 'frm')
    .forEach(key => {
      obj = {
        HeadID: Number(ReactDOM.findDOMNode(this.refs[key]).elements.HeadID.value), 
        FoodCode:ReactDOM.findDOMNode(this.refs[key]).elements.FoodCode.value, 
        SectionID: Number(ReactDOM.findDOMNode(this.refs[key]).elements.SectionID.value),
        Calories: Number(ReactDOM.findDOMNode(this.refs[key]).elements.Calories.value), 
        Protein: Number(ReactDOM.findDOMNode(this.refs[key]).elements.Protein.value), 
        Carbs: Number(ReactDOM.findDOMNode(this.refs[key]).elements.Carbs.value), 
        Fat: Number(ReactDOM.findDOMNode(this.refs[key]).elements.Fat.value),
        Quantity_MeasurementUnitCode: ReactDOM.findDOMNode(this.refs[key]).elements.QuntityMeasurementUnit.value,
        Quantity: Number(ReactDOM.findDOMNode(this.refs[key]).elements.Quantity.value)
      };
    });

    var url = 'api/MobileApi/postEatingGuideDetail';
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
          },
        body: JSON.stringify(obj)
        };
    fetch(url, requestOptions)
        .then(
            (result) => {
                onClose();
            },
            (error) => {
                console.log(error);
            });
    }

    editFood(foodCode, langCode, onItemsRefresh){
      
    }
}
export default EatingGuideDetailEditModal;