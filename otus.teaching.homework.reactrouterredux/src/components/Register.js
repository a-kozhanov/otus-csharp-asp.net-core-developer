import {Button, Container, Form, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import {useForm} from "react-hook-form";
import {useHistory} from "react-router-dom";

const Register = ({registerCallback}) => {

    let history = useHistory();
    const {register, handleSubmit, errors} = useForm();

    async function submitForm(data) {
        const success = registerCallback(data);
        history.push("/home");
    }

    return (
        <Container>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active" aria-current="page">Register</li>
                </ol>
            </nav>

            <Container style={{width: '40%'}}>
                <Form onSubmit={handleSubmit(submitForm)} className="text-left">
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
                        <Button color="primary" type="submit">Зарегистрироваться</Button>
                    </FormGroup>
                </Form>
            </Container>
        </Container>
    );
}

export default Register;