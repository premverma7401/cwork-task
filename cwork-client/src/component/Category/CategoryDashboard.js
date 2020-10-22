import React, { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import agent from '../../api/agent';
import AddCategory from './AddCategory';
import ViewCategory from './ViewCategory';

const CategoryDashboard = () => {
  const [listCategories, setListCategories] = useState([]);
  const [initialValueMessage, setInitialValueMessage] = useState('');
  const [recentCategory, setRecentCategory] = useState({});

  const checkFirstCategoryEntry = async () => {
    try {
      const categoryStatus = await agent.Categories.list();
      console.log(categoryStatus);
      if (categoryStatus.length === 0) {
        setInitialValueMessage(
          'Min Weight should start from 0, as this is your first category in database'
        );
      } else {
        setListCategories(categoryStatus);
      }
    } catch (error) {
      toast.error('Error from API.');
    }
  };

  const lastCategorySaved = async () => {
    try {
      const recentCategory = await agent.Categories.recent();
      setRecentCategory(recentCategory);
      console.log(recentCategory, 'value');
    } catch (error) {}
  };
  useEffect(() => {
    checkFirstCategoryEntry();
    lastCategorySaved();
  }, []);

  return (
    <div>
      <AddCategory
        initialValueMessage={initialValueMessage}
        recent={recentCategory}
        recentCategory={setRecentCategory}
      />
      <ViewCategory categories={listCategories} />
    </div>
  );
};

export default CategoryDashboard;
