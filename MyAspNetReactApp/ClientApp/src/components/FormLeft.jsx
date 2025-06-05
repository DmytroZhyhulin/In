import React from 'react';
import { useForm } from 'react-hook-form';

export default function FormLeft({ onUpdate }) {
  const { register, handleSubmit } = useForm({
    defaultValues: { name: '', position: '', showDetails: true }
  });

  const onSubmit = (data) => {
    onUpdate(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="form-left">
      <div>
        <label>Name</label>
        <input {...register('name')} />
      </div>
      <div>
        <label>Position</label>
        <input {...register('position')} />
      </div>
      <div>
        <label>
          <input type="checkbox" {...register('showDetails')} /> Show details
        </label>
      </div>
      <button type="submit">Apply</button>
    </form>
  );
}
