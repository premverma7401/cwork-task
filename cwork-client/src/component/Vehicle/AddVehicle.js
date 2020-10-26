import React, { useState } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { Button, Form, Label, Segment } from 'semantic-ui-react';
import { history } from '../..';
import agent from '../../api/agent';
import AssignedCategory from '../../common/AssignedCategory';
import ManufacturerList from '../../common/ManufacturerList';

const AddVehicle = () => {
  const initState = {
    ownerName: '',
    manufacturingId: '',
    year: '',
    weight: '',
    categoryId: '',
  };
  const resetState = {
    ownerName: '',
    manufacturingId: null,
    year: '',
    weight: '',
    categoryId: null,
  };
  const [vehicle, setVehicle] = useState(initState);
  const [categoryAssigned, setCategoryAssigned] = useState({
    categoryName: '',
    categoryIcon: '',
    errorText: '',
  });

  const handleChange = (e) => {
    e.preventDefault();
    const vehicleDetails = { ...vehicle };
    vehicleDetails[e.target.name] = e.target.value;
    vehicleDetails.manufacturingId = Number.parseInt(
      vehicleDetails.manufacturingId
    );
    vehicleDetails.weight = Number.parseFloat(vehicleDetails.weight);
    setVehicle(vehicleDetails);
    console.log(vehicle, 'from2');
  };
  const assignCategory = async (number) => {
    if (!vehicle.weight) {
      toast.error('Please insert weight of the vehicle');
    } else {
      try {
        const categoryAssigned = await agent.Categories.getCategoryByWeight(
          number
        );
        console.log(categoryAssigned);
        setCategoryAssigned(categoryAssigned);
        setVehicle({ ...vehicle, categoryId: categoryAssigned.categoryId });
        toast.success(`Category Assigned as ${categoryAssigned.categoryName} `);
      } catch (error) {
        toast.error('No Category available for this Weight.');
        console.log(error);
      }
    }
  };

  const handleSubmit = async () => {
    try {
      await agent.Vehicle.create(vehicle);
      toast.success('Vehicle Added');
      history.push('/user');
      console.log('submitted', vehicle);
    } catch (error) {
      toast.error('Error Occurred');
    }
  };
  return (
    <Segment>
      <Form onSubmit={handleSubmit} autoComplete="off">
        <Form.Field>
          <input
            type="text"
            placeholder="Owner Name"
            value={vehicle.name}
            onChange={handleChange}
            name="ownerName"
            required
          />
        </Form.Field>
        <Form.Field>
          <input
            type="number"
            placeholder="Year"
            value={vehicle.year}
            onChange={handleChange}
            name="year"
            required
            min="1980"
            max="2022"
            step="1"
          />
        </Form.Field>
        <Form.Field>
          <ManufacturerList
            name="manufacturingId"
            onChange={handleChange}
            value={vehicle.manufacturingId}
          />
        </Form.Field>

        <Form.Field inline>
          <input
            type="number"
            placeholder="Weight"
            value={vehicle.weight}
            onChange={handleChange}
            name="weight"
            required
          />

          <Label
            as="a"
            basic
            color="blue"
            pointing="left"
            onClick={() => assignCategory(vehicle.weight)}
          >
            Assign Category
          </Label>
        </Form.Field>
        <AssignedCategory
          name="category"
          onChange={handleChange}
          value={categoryAssigned.categoryName}
        />

        <ToastContainer />

        <div>
          <Button
            icon="plus"
            content="Submit"
            primary
            disabled={!vehicle.categoryId || !vehicle.manufacturingId}
          />
        </div>
      </Form>
    </Segment>
  );
};

export default AddVehicle;
