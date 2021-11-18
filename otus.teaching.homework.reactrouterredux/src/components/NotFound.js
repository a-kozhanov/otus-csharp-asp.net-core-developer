import {Alert, Container} from "reactstrap";
import {Link} from "react-router-dom";


const NotFound = () => {
    return (
        <Container>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active" aria-current="page">404</li>
                </ol>
            </nav>
            <Alert color="danger">
                Страница не найдена. <Link to="/home">На главную</Link>.
            </Alert>
        </Container>
    );
}

export default NotFound;