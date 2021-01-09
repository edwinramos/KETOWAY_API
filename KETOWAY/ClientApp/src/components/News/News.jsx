import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Icon from "@material-ui/core/Icon";
import BootstrapTable from 'react-bootstrap/Table'
import EditModal from './NewsModal';

class News extends Component {
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
                <Card>
                    <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                        <div style={{ float: 'left' }}>
                            <h4 className={this.state.classes.cardTitleWhite}>
                                Tabla de Novedades
                        </h4>
                            <p className={this.state.classes.cardCategoryWhite}>
                                Lista de novedades para la app.
                        </p>
                        </div>
                        <Button color="info" style={{ float: 'right' }} onClick={() => this.openEditModal("0")}><Icon>add_circle_outline</Icon> Agregar</Button>
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
                                        <td>{item.newsCode}</td>
                                        <td>{item.newsTitle}</td>
                                        <td>
                                            <div>
                                                <Button onClick={() => this.openEditModal(item.newsCode)} color="info"><Icon>edit</Icon> Editar</Button>
                                                <Button onClick={() => this.deleteRecipe(item.newsCode)} color="danger"><Icon>delete</Icon> Eliminar</Button>
                                            </div>
                                        </td>
                                    </tr>
                                })}
                            </tbody>
                        </BootstrapTable>
                    </Card.Body>
                </Card>
                <EditModal key={"01"} onUpdate={this.updateData} onClose={this.closeEditModal} isOpen={this.state.recipeEditing} dataList={this.state.recipesToEdit} />
            </div>
        );
    }
    getRecipes() {
        fetch("api/MobileApi/getAllNews")
            .then(res => res.json())
            .then(
                (result) => {
                    //   var arr = Object.keys(result).map(function(key) {
                    //     return [result[key].recipeCode,result[key].recipeTitle,<div><Button onClick={()=> openEditModal()} color="info"><Icon>edit</Icon> Editar</Button><Button color="danger"><Icon>delete</Icon> Eliminar</Button></div>];
                    //   });
                    this.setState({ recipes: result });
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    onRecipeChange(arr) {
        console.log(arr);

        this.setState({ recipesToPost: arr });
        console.log(this.state.recipesToPost);
    }
    openEditModal(code) {
        fetch("api/MobileApi/getNews/" + code)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({ recipesToEdit: result, recipeEditing: true });
                },
                (error) => {
                    console.log(error);
                }
            );
    }
    closeEditModal() {
        this.setState({ recipeEditing: false })
    }
    updateData(arr) {
        console.log(arr);
        var url = 'api/MobileApi/postNews';
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
                    this.getRecipes();
                    this.setState({ recipesToPost: [], recipeEditing: false });
                },
                (error) => {
                    console.log(error);
                });
    }
    deleteRecipe(code) {
        var url = 'api/MobileApi/deleteNews';
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(code)
        };
        fetch(url, requestOptions)
            .then(
                (result) => {
                    this.getRecipes();
                },
                (error) => {
                    console.log(error);
                });
    }
}
export default News;