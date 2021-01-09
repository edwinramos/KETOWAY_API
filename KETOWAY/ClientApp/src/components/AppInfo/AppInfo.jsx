import React, { Component } from 'react';
import Card from 'react-bootstrap/Card';
import JoditEditor from "jodit-react";
import Accordion from 'react-bootstrap/Accordion'
import Icon from "@material-ui/core/Icon";
import Button from 'react-bootstrap/Button';


var aboutInfoSend = [];
var referenceInfoSend = [];
class AppInfo extends Component {
    state = {
        classes: { cardCategoryWhite: "makeStyles-cardCategoryWhite-250", cardTitleWhite: "makeStyles-cardTitleWhite-251" },
        aboutInfo: [], referenceInfo: []
    };

    constructor() {
        super();
        fetch("api/MobileApi/appInfo")
            .then(res => res.json())
            .then(
                (result) => {
                    //var languages = getLanguages();

                    var arr = [];
                    var refs = [];
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].infoCode === 'about')
                            arr.push(result[i]);

                        if (result[i].infoCode === 'reference')
                            refs.push(result[i]);
                    }
                    aboutInfoSend = arr;
                    this.setState({ aboutInfo: arr, referenceInfo: refs });
                },
                (error) => { console.log(error); }
            );
        this.updateAboutInfo = this.updateAboutInfo.bind(this)
        this.updateReferenceInfo = this.updateReferenceInfo.bind(this)
        this.postAboutInfo = this.postAboutInfo.bind(this)
        this.postReferenceInfo = this.postReferenceInfo.bind(this)
    }

    render() {
        return (<div >
            <Card style={{ width: '100%' }}>
                <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                    <div style={{ float: 'left' }}>
                        <h4 >Acerca de...</h4>
                        <p >
                            Informacion util de la app.
                        </p>
                        <Button color="info" style={{ float: 'right' }} onClick={this.postAboutInfo}><Icon>publish</Icon> Publicar</Button>
                    </div>
                </Card.Header>
                <Card.Body>
                    {this.state.aboutInfo.map((obj, key) => (<Accordion key={"a" + obj.langCode}> <Card key={"a" + obj.langCode}>
                        <Card.Header>
                            <Accordion.Toggle as={Button} variant="link" eventKey="0">
                                Sobre KetoWay ({obj.langCode})
                          </Accordion.Toggle>
                        </Card.Header>
                        <Accordion.Collapse eventKey="0">
                            <Card.Body>
                                <JoditEditor key={"a" + obj.langCode} config={{ toolbarButtonSize: 'small', useSplitMode: true }} value={obj.infoContent} onChange={(newContent) => this.updateAboutInfo(newContent, obj.langCode)} />
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card> </Accordion>))}
                </Card.Body>
            </Card>
            <Card style={{ width: '100%' }}>
                <Card.Header variant="top" src="holder.js/100px180?text=Image cap">
                    <div style={{ float: 'left' }}>
                        <h4 >Referencias</h4>
                        <p >
                            Referencias profesionales que respalden la app.
                        </p>
                        <Button color="info" style={{ float: 'right' }} onClick={this.postReferenceInfo}><Icon>publish</Icon> Publicar</Button>
                    </div>
                </Card.Header>
                <Card.Body>
                    {this.state.referenceInfo.map((x, key) => (<Accordion key={"b" + key}> <Card >
                        <Card.Header>
                            <Accordion.Toggle as={Button} variant="link" eventKey="0">
                                Referencias Personales ({x.langCode})
                          </Accordion.Toggle>
                        </Card.Header>
                        <Accordion.Collapse eventKey="0">
                            <Card.Body>
                                <JoditEditor key={"b" + x.langCode} config={{ toolbarButtonSize: 'small' }} value={x.infoContent} onChange={(newContent) => this.updateReferenceInfo(newContent, x.langCode)} />
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card> </Accordion>))}
                </Card.Body>
            </Card>
        </div>
        );
    }
    postAboutInfo() {
        this.setState({
            aboutInfo: aboutInfoSend
        });
        var arr = this.state.aboutInfo;
        for (var i in arr) {
            var url = 'api/MobileApi/postInfo';
            const requestOptions = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(arr[i])
            };
            fetch(url, requestOptions)
                .then(
                    (result) => {
                        console.log(result);
                    },
                    (error) => {
                        console.log(error);
                    });
        }
    }
    postReferenceInfo() {
        this.setState({
            referenceInfo: referenceInfoSend
        });
        var arr = this.state.referenceInfo;
        for (var i in arr) {
            var url = 'api/MobileApi/postInfo';
            const requestOptions = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(arr[i])
            };
            fetch(url, requestOptions)
                .then(
                    (result) => {
                        console.log(result);
                    },
                    (error) => {
                        console.log(error);
                    });
        }
    }

    updateAboutInfo(value, langCode) {
        var arr = aboutInfoSend;
        for (var i in arr) {
            if (arr[i].langCode == langCode && arr[i].infoCode == "about") {
                arr[i].infoContent = value;
                break; //Stop this loop, we found it!
            }
        }

        aboutInfoSend = arr;
    }

    updateReferenceInfo(value, langCode) {
        var arr = this.state.referenceInfo;
        for (var i in arr) {
            if (arr[i].langCode == langCode && arr[i].infoCode == "reference") {
                arr[i].infoContent = value;
                break; //Stop this loop, we found it!
            }
        }

        referenceInfoSend = arr;
    }
}
export default AppInfo;