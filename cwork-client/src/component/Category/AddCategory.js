import React, { Fragment, useState } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { Button, Form, Message } from 'semantic-ui-react';
import agent from '../../api/agent';
import '../../layout/App.css';

const AddCategory = ({ initialValueMessage, recent }) => {
  const categoryInitState = {
    categoryName: '',
    minWeight: '',
    maxWeight: '',
  };
  const [category, setCategory] = useState(categoryInitState);

  const handleChange = (e) => {
    e.preventDefault();
    const categoryValue = { ...category };
    categoryValue[e.target.name] = e.target.value;
    categoryValue.minWeight = Number.parseFloat(recent.maxWeight + 1);
    categoryValue.maxWeight = Number.parseFloat(categoryValue.maxWeight);
    setCategory(categoryValue);
    console.log(category);
  };

  const handleSubmit = () => {
    if (category.minWeight >= category.maxWeight) {
      setCategory({
        ...category,
        minWeight: category.minWeight,
        maxWeight: (category.maxWeight = category.minWeight + 1),
      });
      console.log(category);
      toast.error(`Max weight should be greater than min weight`);
    } else {
      setCategory({
        ...category,
        categoryName: category.categoryName,
        maxWeight: Number(category.maxWeight),
        minWeight: parseFloat(category.minWeight),
      });
      agent.Categories.create(category);
      toast.success('Category added!!');
      // listCategories.push(category);
      setCategory(categoryInitState);
    }
  };
  return (
    <Fragment>
      <h1>Category CRUD</h1>

      <Form
        className="category-plus-wrapper"
        onSubmit={handleSubmit}
        autoComplete="off"
      >
        <div>
          <div className="category-info">
            <input
              type="text"
              placeholder="Category Name"
              value={category.categoryName}
              onChange={handleChange}
              name="categoryName"
            />
            <input
              type="number"
              placeholder="Min Weight"
              value={category.minWeight}
              onChange={handleChange}
              name="minWeight"
            />
            <input
              type="number"
              placeholder="Max Weight"
              value={category.maxWeight}
              onChange={handleChange}
              name="maxWeight"
            />
          </div>
          {initialValueMessage ? (
            <Message content={initialValueMessage}></Message>
          ) : (
            <></>
          )}
          <ToastContainer />
        </div>

        <div>
          <Button
            icon="plus"
            content="Submit"
            primary
            disabled={!category.maxWeight}
          />
        </div>
      </Form>
    </Fragment>
  );
};

export default AddCategory;
