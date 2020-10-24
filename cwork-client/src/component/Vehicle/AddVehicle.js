import React, { useState } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { Button, Form, Label, Message } from 'semantic-ui-react';
import agent from '../../api/agent';
import AssignedCategory from '../../common/AssignedCategory';
import ManufacturerList from '../../common/ManufacturerList';

const AddVehicle = () => {
  const [vehicle, setVehicle] = useState({
    ownerName: '',
    manufacturer: '',
    year: '',
    weight: '',
    category: '',
  });
  const [categoryAssigned, setCategoryAssigned] = useState({
    categoryName: '',
    categoryIcon: '',
    errorText: '',
  });

  const handleChange = (e) => {
    e.preventDefault();
    const vehicleDetails = { ...vehicle };
    vehicleDetails[e.target.name] = e.target.value;
    if (vehicleDetails.weight) {
      assignCategory(vehicleDetails.weight);
      console.log('this is fired from onchANGE');
    }
    setVehicle(vehicleDetails);
    console.log(vehicle, 'from2');
  };
  const assignCategory = async (number) => {
    try {
      const categoryAssigned = await agent.Categories.getCategoryByWeight(
        number
      );
      if (categoryAssigned === 'Not Found') {
        // setCategoryAssigned({
        //   ...categoryAssigned,
        //   errorText: 'No Category assigned for this range',
        // });
        console.log('not found');
      } else {
        setCategoryAssigned(categoryAssigned);
      }
      //   toast.success(`Category Assigned as ${categoryAssigned.categoryName} `);
    } catch (error) {
      console.log(error);
    }
  };

  const handleSubmit = () => {
    console.log('submitted');
  };
  return (
    <div>
      <Form onSubmit={handleSubmit} autoComplete="off">
        <Form.Field>
          <input
            type="text"
            placeholder="Owner Name"
            value={vehicle.name}
            onChange={handleChange}
            name="ownerName"
          />
        </Form.Field>
        <Form.Field>
          <input
            type="number"
            placeholder="Year"
            value={vehicle.year}
            onChange={handleChange}
            name="year"
          />
        </Form.Field>
        <Form.Field>
          <ManufacturerList />
        </Form.Field>
        <Form.Field>
          <input
            type="number"
            placeholder="Weight"
            value={vehicle.weight}
            onChange={handleChange}
            name="weight"
          />
        </Form.Field>

        <Form.Field>
          <Message>
            <Label as="a" color="teal" floating>
              Assign Category
            </Label>
          </Message>
          <AssignedCategory
            name={categoryAssigned.categoryName}
            error={categoryAssigned.errorText}
          />
        </Form.Field>

        <ToastContainer />

        <div>
          <Button icon="plus" content="Submit" primary />
        </div>
      </Form>
    </div>
  );
};

export default AddVehicle;
