import {Container} from "reactstrap";

const HomePage = ({text}) => {
    const styleTextArea = {
        width: '100%',
        height: '130px',
        resize: 'none',
        borderStyle: 'none',
        borderColor: 'Transparent',
        overflow: 'auto',
        border: 'none',
        outline: 'none',
        webkitBoxShadow: 'none',
        mozBoxShadow: 'none',
        boxShadow: 'none',
    };

    return (
        <Container>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active" aria-current="page">Home</li>
                </ol>
            </nav>

            <textarea style={styleTextArea} readOnly={true} unselectable={true}>
                {text}
            </textarea>
        </Container>
    );
}

export default HomePage;