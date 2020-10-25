import _ from 'lodash';
import React, { useEffect, useReducer, useState } from 'react';
import { toast } from 'react-toastify';
import { Table } from 'semantic-ui-react';
import agent from '../../api/agent';

const tableData = [
  { name: 'John', age: 15, age: 15, age: 15, gender: 'Male' },
  { name: 'Amber', age: 40, gender: 'Female' },
  { name: 'Leslie', age: 25, gender: 'Other' },
  { name: 'Ben', age: 70, gender: 'Male' },
];

function exampleReducer(state, action) {
  switch (action.type) {
    case 'CHANGE_SORT':
      if (state.column === action.column) {
        return {
          ...state,
          data: state.data.reverse(),
          direction:
            state.direction === 'ascending' ? 'descending' : 'ascending',
        };
      }

      return {
        column: action.column,
        data: _.sortBy(state.data, [action.column]),
        direction: 'ascending',
      };
    default:
      throw new Error();
  }
}

const UserDashboard = () => {
  const [state, dispatch] = useReducer(exampleReducer, {
    column: null,
    data: tableData,
    direction: null,
  });
  const [vehicleData, setVehicleData] = useState([]);
  const { column, data, direction } = state;

  const getVehicleData = async () => {
    try {
      const vehicleData = await agent.Vehicle.list();
      console.log(vehicleData);
      setVehicleData(vehicleData);
      toast.success('Vehicle data received');
    } catch (error) {
      toast.error('Error Occured');
    }
  };
  useEffect(() => {
    getVehicleData();
  }, []);
  return (
    <Table sortable celled fixed>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell
            sorted={column === 'name' ? direction : null}
            onClick={() => dispatch({ type: 'CHANGE_SORT', column: 'name' })}
          >
            Owner Name
          </Table.HeaderCell>
          <Table.HeaderCell
            sorted={column === 'age' ? direction : null}
            onClick={() => dispatch({ type: 'CHANGE_SORT', column: 'age' })}
          >
            Manufacturer
          </Table.HeaderCell>
          <Table.HeaderCell
            sorted={column === 'gender' ? direction : null}
            onClick={() => dispatch({ type: 'CHANGE_SORT', column: 'gender' })}
          >
            Year of Manufacture
          </Table.HeaderCell>
          <Table.HeaderCell
            sorted={column === 'gender' ? direction : null}
            onClick={() => dispatch({ type: 'CHANGE_SORT', column: 'gender' })}
          >
            Weight
          </Table.HeaderCell>
          <Table.HeaderCell
            sorted={column === 'gender' ? direction : null}
            onClick={() => dispatch({ type: 'CHANGE_SORT', column: 'gender' })}
          >
            Category
          </Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {vehicleData.map(
          ({ ownerName, manufacturerName, categoryIcon, year, weight }, i) => (
            <Table.Row key={i}>
              <Table.Cell>{ownerName}</Table.Cell>
              <Table.Cell>{manufacturerName}</Table.Cell>
              <Table.Cell>{year}</Table.Cell>
              <Table.Cell>{weight}</Table.Cell>
              <Table.Cell>{categoryIcon ? 'icon' : 'no Icons'}</Table.Cell>
            </Table.Row>
          )
        )}
      </Table.Body>
    </Table>
  );
};

export default UserDashboard;
