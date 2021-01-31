import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Icon from "@material-ui/core/Icon";
import BootstrapTable from 'react-bootstrap/Table'
import EditModal from './FastingGuideModal';
import { PostData, GetData, DeleteData } from "../Helper";

class FastingGuide extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        recipes: [], recipeEditing: false, recipesToEdit: [], recipesToPost: []
    };

    constructor() {
        super();
        this.getRecipes();

        this.closeEditModal = this.closeEditModal.bind(this);
        this.onRecipeChange = this.onRecipeChange.bind(this);
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
                            <h4>Guias de Ayuno</h4>
                            <p>
                                Guias de ayuno para la app.
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
                                        <td>{item.fastingCode}</td>
                                        <td>{item.fastingTitle}</td>
                                        <td>
                                            <div>
                                                <Button onClick={() => this.openEditModal(item.fastingCode)} color="info"><Icon>edit</Icon> Editar</Button>
                                                <Button onClick={() => this.deleteRecipe(item.fastingCode)} className="btn btn-primary"><Icon>delete</Icon> Eliminar</Button>
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
    async getRecipes() {
        var url = "api/FastingGuide";
        var result = await GetData(url);
        if (result) {
            this.setState({ recipes: result });
        }
    }

    onRecipeChange(arr) {
        console.log(arr);

        this.setState({ recipesToPost: arr });
        console.log(this.state.recipesToPost);
    }
    async openEditModal(code) {
        var url = "api/FastingGuide/" + code;
        var result = await GetData(url);
        if (result) {
            this.setState({ recipesToEdit: result, recipeEditing: true });
        }
    }
    closeEditModal() {
        this.setState({ recipeEditing: false })
    }
    async updateData(arr) {
        console.log(arr);
        var url = 'api/FastingGuide';
        var apiRequest = { payload: arr };
        var result = await PostData(url, apiRequest);
        if (result) {
            this.getRecipes();
            this.setState({ recipesToPost: [], recipeEditing: false });
        }
    }
    async deleteRecipe(code) {
        var url = 'api/FastingGuide/'+code;
        var result = await DeleteData(url);
        if (result) {
            this.getRecipes();
        }
    }
}
export default FastingGuide;