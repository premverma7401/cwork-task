import React from 'react';
import { Message } from 'semantic-ui-react';

const AssignedCategory = ({ value }) => {
  return (
    <Message icon="inbox" header={` Category Assigned : ${value}`} compact />
  );
};

export default AssignedCategory;
