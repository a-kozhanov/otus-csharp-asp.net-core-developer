import {Alert, Button, Container, Form, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import {useForm} from "react-hook-form";
import {Link, useHistory} from "react-router-dom";
import {useState} from "react";

const LogIn = ({loginCallback}) => {

    const [isCredentialsInvalid, setInvalidCredentials] = useState(false);
    const {register, handleSubmit, errors} = useForm();
    let history = useHistory();

    const submitFormAsync = async (data) => {
        const success = await loginCallback(data);

        if (success) {
            history.push("/home");
        }

        setInvalidCredentials(!success);
    }

    let showAlert = (
        <Alert color="danger">
            Неправильный логин или пароль. <Link to="/home">На главную</Link>.
        </Alert>
    )

    return (
        <Container>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active" aria-current="page">Login</li>
                </ol>
            </nav>

            {isCredentialsInvalid && showAlert}

            <Container style={{width: '40%'}}>
                <Form onSubmit={handleSubmit(submitFormAsync)} className="text-left">
                    <FormGroup>
                        <Label for="email">Введите email</Label>
                        <Input type="email" name="email" id="email"
                               innerRef={register({required: true})}
                               invalid={errors.email}
                        />
                        {errors.email && <FormFeedback>Данное поле обязательно</FormFeedback>}
                    </FormGroup>

                    <FormGroup>
                        <Label for="psw">Пароль</Label>
                        <Input type="password" name="password" id="psw"
                               innerRef={register({required: true})}
                               invalid={errors.password}
                        />
                        {errors.password && <FormFeedback>Данное поле обязательно</FormFeedback>}
                    </FormGroup>

                    <FormGroup className="text-center">
                        <Button color={'success'} type="submit">Войти</Button>
                    </FormGroup>
                </Form>
            </Container>
        </Container>
    );
}

export default LogIn;