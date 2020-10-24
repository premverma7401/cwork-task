import React from 'react';

const AssignedCategory = ({ name, error }) => {
  return (
    <div className="ui icon message">
      <i aria-hidden="true" className="inbox icon"></i>
      <div className="content">
        <div className="header">
          Category Assigned : {name}
          {error}
        </div>
      </div>
    </div>
  );
};

export default AssignedCategory;
