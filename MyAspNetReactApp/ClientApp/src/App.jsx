import React, { useRef, useState } from 'react';
import FormLeft from './components/FormLeft';
import TemplateRight from './components/TemplateRight';
import html2pdf from 'html2pdf.js';
import { saveAs } from 'file-saver';
import PizZip from 'pizzip';
import Docxtemplater from 'docxtemplater';

export default function App() {
  const [formData, setFormData] = useState({
    name: '',
    position: '',
    showDetails: true
  });
  const templateRef = useRef();

  const exportPdf = () => {
    const opt = { margin: 1, filename: 'template.pdf' };
    html2pdf().from(templateRef.current).set(opt).save();
  };

  const exportDocx = () => {
    const template = `\
      <w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
        <w:body>
          <w:p><w:r><w:t>Name: ${formData.name}</w:t></w:r></w:p>
          <w:p><w:r><w:t>Position: ${formData.position}</w:t></w:r></w:p>
        </w:body>
      </w:document>`;
    const zip = new PizZip();
    zip.file('word/document.xml', template);
    const doc = new Docxtemplater(zip, { paragraphLoop: true, linebreaks: true });
    const blob = doc.getZip().generate({ type: 'blob', mimeType: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });
    saveAs(blob, 'template.docx');
  };

  return (
    <div className="app">
      <FormLeft onUpdate={setFormData} />
      <div className="actions">
        <button onClick={exportPdf}>Export PDF</button>
        <button onClick={exportDocx}>Export DOCX</button>
      </div>
      <TemplateRight data={formData} ref={templateRef} />
    </div>
  );
}
