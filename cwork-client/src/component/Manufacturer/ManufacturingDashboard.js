import React from 'react';
import { Grid } from 'semantic-ui-react';
import AddManufacturer from './AddManufacturer';
import ViewManufacturer from './ViewManufacturer';

const ManufacturingDashboard = () => {
  return (
    <div>
      <Grid>
        <Grid.Row>
          <Grid.Column width={5}>
            <AddManufacturer />
          </Grid.Column>
          <Grid.Column width={6}>
            <ViewManufacturer />
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </div>
  );
};

export default ManufacturingDashboard;
