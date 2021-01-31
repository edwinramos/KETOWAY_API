import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Icon from "@material-ui/core/Icon";
import BootstrapTable from 'react-bootstrap/Table'
import EditModal from './FoodModal';
import { PostData, GetData, DeleteData } from "../Helper";

export class Food extends Component {
    static displayName = Food.name;

    constructor(props) {
        super(props);
        this.state = { recipeEditing: false, recipes: [], recipesToEdit: [] };
        this.getRecipes(true);

        this.closeEditModal = this.closeEditModal.bind(this);
        //this.onRecipeChange = this.onRecipeChange.bind(this);
        this.updateData = this.updateData.bind(this);
        this.deleteRecipe = this.deleteRecipe.bind(this);
        this.getRecipes = this.getRecipes.bind(this);
    }

    render() {
        return (
            <div>
                <Card style={{ width: '100%' }}>
                    <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                        <div style={{ float: 'left' }}>
                            <h4>Alimentos Permitidos</h4>
                            <p>
                                Lista de Alimentos Permitidos para la app.
                        </p>
                            <Button color="info" style={{ float: 'right' }} onClick={() => this.openEditModal("0")}><Icon>add_circle_outline</Icon> Agregar</Button>
                        </div>
                    </Card.Header>
                    <Card.Body>
                        <BootstrapTable responsive>
                            <thead>
                                <tr>
                                    <th>Codigo</th>
                                    <th>Titulo</th>
                                    <th>#</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.recipes.map((item) => {
                                    return <tr>
                                        <td>{item.foodCode}</td>
                                        <td>{item.foodTitle}</td>
                                        <td>
                                            <div>
                                                <Button onClick={() => this.openEditModal(item.foodCode)} color="info"><Icon>edit</Icon> Editar</Button>
                                                <Button onClick={() => this.deleteRecipe(item.foodCode)} className="btn btn-primary"><Icon>delete</Icon> Eliminar</Button>
                                            </div>
                                        </td>
                                    </tr>
                                })}
                            </tbody>
                        </BootstrapTable>
                    </Card.Body>
                </Card>
                <EditModal onUpdate={this.updateData} onClose={this.closeEditModal} isOpen={this.state.recipeEditing} dataList={this.state.recipesToEdit} />
            </div>
        );
    }
    async updateData(arr) {
        var url = 'api/Food';
        var apiRequest = { payload: arr };
        var result = await PostData(url, apiRequest);

        if (result) {
            this.getRecipes(true);
            this.setState({ recipesToPost: [], recipeEditing: false });
        }
    }

    async deleteRecipe(code) {
        var url = 'api/Food/' + code;
        var result = await DeleteData(url);
        if (result) {
            this.getRecipes(true);
        }
    }

    async getRecipes(isAllowed) {
        var url = "api/Food/getFoods/" + isAllowed;
        var result = await GetData(url);
        if (result) {
            this.setState({ recipes: result });
        }
    }
    async openEditModal(foodCode) {
        var url = "api/Food/" + foodCode;
        var result = await GetData(url);
        if (result) {
            this.setState({ recipesToEdit: result, recipeEditing: true });
        }
    }
    closeEditModal() {
        this.setState({ recipeEditing: false })
    }
}
