import React, { useState } from 'react';

const ManufacturerList = ({ name, onChange }) => {
  const [manufacturers, setManufacturers] = useState([
    { key: 'c', text: 'Honda', value: '0' },
    { key: 'p', text: 'Audi', value: '1' },
    { key: 'f', text: 'Toyota', value: '2' },
    { key: 't', text: 'Holden', value: '3' },
  ]);
  return (
    <select htmlFor="manufacturers" name={name} onChange={onChange}>
      <option value="">Select Manufacturer</option>
      {manufacturers.map((manufacturer) => (
        <option key={manufacturer.key} value={manufacturer.value}>
          {manufacturer.text}
        </option>
      ))}
    </select>
  );
};

export default ManufacturerList;
