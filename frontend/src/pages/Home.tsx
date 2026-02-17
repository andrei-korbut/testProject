import React from 'react';

const Home: React.FC = () => {
  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 flex items-center justify-center">
      <div className="text-center">
        <h1 className="text-5xl font-bold text-gray-800 mb-4">
          Product Management App
        </h1>
        <p className="text-gray-600 text-lg">
          Your products, managed efficiently
        </p>
      </div>
    </div>
  );
};

export default Home;
