import React, { Fragment, useEffect, useState } from 'react';
import { Image, List, Segment } from 'semantic-ui-react';
import agent from '../../api/agent';

const ViewManufacturer = () => {
  const [manufacturers, setManufacturers] = useState([]);

  const getManufacturerList = async () => {
    try {
      const manufacturers = await agent.Manufacturer.list();
      console.log(manufacturers);
      setManufacturers(manufacturers);
    } catch (error) {}
  };
  useEffect(() => {
    getManufacturerList();
  }, []);

  return (
    <Segment>
      <h3> View Manufacturers</h3>
      <List animated size="huge" selection divided>
        {manufacturers.map((manufacturer) => (
          <List.Item key={manufacturer.id}>
            <Image avatar src="https://loremflickr.com/420/420/car" />
            <List.Content>
              <List.Header>{manufacturer.manufacturerName}</List.Header>
            </List.Content>
          </List.Item>
        ))}
      </List>
    </Segment>
  );
};

export default ViewManufacturer;
