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
import Table from 'react-bootstrap/Table'
import EditModal from './EatingGuideDetailEditModal';

class EatingGuideDetailModal extends Component {
    state = { classes:{cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251"},
            items:[], openEditModal: false, objEdit: {}
          };
          constructor(props) {
            super(props);

            this.openEditModal = this.openEditModal.bind(this);
            this.closeEditModal = this.closeEditModal.bind(this);
            this.deleteFood = this.deleteFood.bind(this);
            this.handleContents = this.handleContents.bind(this);
          }
    render() {
      const { onClose, onItemsRefresh, items, langCode, isOpen } = this.props // destructure

        return (
            <Modal
          size="xl"
          show={isOpen}
          onHide={() => onClose()}
          aria-labelledby="example-modal-sizes-title-lg">
          <Modal.Header closeButton>
                    <Modal.Title id="example-modal-sizes-title-lg">
                        Alimentos
            </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Button onClick={() => { this.openEditModal(onItemsRefresh) }} color="info"><Icon>add </Icon></Button>

                  <Table responsive>
                    <thead>
                      <tr>
                        <th>Alimento</th>
                        <th>Grupo</th>
                        <th>Seccion</th>
                        <th>Cantidad</th>
                        <th>Calorias(kcal)</th>
                        <th>Carbohidratos(g)</th>
                        <th>Proteinas(g)</th>
                        <th>Fat(g)</th>
                        <th>#</th>
                      </tr>
                    </thead>
                    <tbody>
                    {items.map((item) => {
                                return <tr>
                                <td>{item.foodDescription}</td>
                                <td>{item.foodGroupDescription}</td>
                                <td>{item.sectionDescription}</td>
                                <td>{item.QuantityDesc}</td>
                                <td>{item.calories}</td>
                                <td>{item.carbs}</td>
                                <td>{item.protein}</td>
                                <td>{item.fat}</td>
                                <td>
                                  <div>
                                    <Button onClick={()=>this.editFood(item, onItemsRefresh)} color="info"><Icon>edit</Icon></Button>
                                    <Button onClick={()=>this.deleteFood(item.foodCode, item.langCode, onItemsRefresh)} color="danger"><Icon>delete</Icon></Button>
                                  </div>
                                </td>
                              </tr>
                    })}
                            <EditModal onClose={() => { this.closeEditModal(onItemsRefresh) }} objEdit={this.state.objEdit} langCode={langCode} isOpen={this.state.openEditModal} />
                    </tbody>
                  </Table>
                </Modal.Body>
            </Modal>
            );
    }

    handleContents(code, langCode, title, value){
      var arr = this.state.recipesToPost;
        var obj = {FoodCode: code, FoodTitle:title, FoodGroupID: 0, FoodContent: value, LangCode:langCode, IsAllowed:true};
        var m = arr.find(x => x.LangCode === langCode);
        if(!m){
          obj.FoodCode = code;
          obj.FoodTitle = title;
          obj.FoodContent = value;
          obj.LangCode = langCode;
          arr.push(obj);
        }
        else{
          obj.FoodTitle = m.FoodTitle;
          obj.FoodGroupID = m.FoodGroupID;
          var index = arr.findIndex(x => x.LangCode === langCode);
          arr[index] = obj;
        }        
        
        this.setState({recipesToPost:arr});
    }

    openEditModal(onItemsRefresh){
      var obj = {
        headId: 1,
        langCode: this.state.langCode,
        quantity: 0,
        carbs: 0,
        fat: 0,
        protein: 0,
        calories: 0
      }
      onItemsRefresh();
      this.setState({openEditModal:true, objEdit: obj});
    }

    closeEditModal(onItemsRefresh){
      onItemsRefresh();
      this.setState({openEditModal:false})
    }

    deleteFood(foodCode, langCode, onItemsRefresh){
      var url = 'api/MobileApi/deleteEatingGuideDetail';
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(foodCode)
            };
        fetch(url, requestOptions)
            .then(
                (result) => {
                  onItemsRefresh(1, langCode);
                },
                (error) => {
                    console.log(error);
                });
    }

    editFood(item, onItemsRefresh){
      this.setState({openEditModal:true, objEdit: item});
    }
}
export default EatingGuideDetailModal;