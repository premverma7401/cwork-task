import React from 'react';
import { Button } from 'semantic-ui-react';
import { toast, ToastContainer } from 'react-toastify';
import agent from '../../api/agent';

const ViewCategory = ({ categories }) => {
  const handleDelete = (e, id) => {
    e.preventDefault();
    console.log(id);
    toast.error('You have deleted the category...');
    agent.Categories.delete(id).then(() => {});
  };

  return (
    <div style={{ marginTop: '30px' }}>
      <h3>View Categories</h3>
      <table className="ui celled table">
        <thead>
          <tr>
            <th>Category Name</th>
            <th>Min Weight</th>
            <th>Max Weight</th>
            <th>Options</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category, i) => (
            <tr key={i}>
              <td data-label="categoryName">{category.categoryName}</td>
              <td data-label="minWeight">{category.minWeight}</td>
              <td data-label="maxWeight">{category.maxWeight}</td>
              <td data-label="delete">
                <Button
                  icon="minus"
                  color="red"
                  onClick={(e) => handleDelete(e, category.categoryId)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <ToastContainer />
    </div>
  );
};

export default ViewCategory;
