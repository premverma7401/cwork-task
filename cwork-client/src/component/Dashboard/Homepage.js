import React from 'react';
import { Container, Segment, Header, Button, Image } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const Homepage = () => {
  return (
    <Segment inverted textAlign="center" vertical className="masthead">
      <Container text>
        <Header as="h1" inverted>
          <Image
            size="massive"
            src="/assets/logo.png"
            alt="logo"
            style={{ marginBottom: 12 }}
          />
          CreditWorks
        </Header>
        <Header as="h2" inverted content="Welcome to Credit Works" />
        <Button as={Link} to="/user" size="huge" inverted>
          Take me to the dashboard!
        </Button>
      </Container>
    </Segment>
  );
};

export default Homepage;
