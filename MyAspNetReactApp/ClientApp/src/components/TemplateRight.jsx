import React, { forwardRef } from 'react';

const TemplateRight = forwardRef(({ data }, ref) => {
  return (
    <div className="template-right" ref={ref}>
      <h2>Employee Card</h2>
      <p><strong>Name:</strong> {data.name}</p>
      <p><strong>Position:</strong> {data.position}</p>
      {data.showDetails && (
        <div className="details">
          <p>Here could be more detailed information...</p>
        </div>
      )}
    </div>
  );
});

export default TemplateRight;
