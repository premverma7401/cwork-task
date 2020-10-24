import React from 'react';
import { Container } from 'semantic-ui-react';

import CategoryDashboard from '../component/Category/CategoryDashboard';
import Dashboard from '../component/Dashboard/Dashboard';
import AddVehicle from '../component/Vehicle/AddVehicle';

const App = () => {
  return (
    <Container>
      <CategoryDashboard />
      <Dashboard />
      <AddVehicle />
    </Container>
  );
};

export default App;
