import React from 'react';
import { Grid } from 'semantic-ui-react';
import AddVehicle from './AddVehicle';
import VehicleViewCard from './VehicleViewCard';

const VehicleDashboard = () => {
  return (
    <div>
      <Grid columns="equal">
        <Grid.Row>
          <Grid.Column width={6}>
            <AddVehicle />
          </Grid.Column>
          <Grid.Column>
            <VehicleViewCard />
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </div>
  );
};

export default VehicleDashboard;
