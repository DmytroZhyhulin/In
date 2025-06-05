# ASP.NET + React Example

This repository shows a minimal example of an ASP.NET Core project with a React client application.
The client renders a form on the left and a document template on the right. Values typed in the form
control the template. Buttons allow exporting the template area to PDF and DOCX on the client side.

```
MyAspNetReactApp/
  ClientApp/       - React frontend
    src/
      components/
        FormLeft.jsx
        TemplateRight.jsx
      App.jsx
      index.js
      index.css
    package.json
    webpack.config.js
    public/index.html
  Controllers/     - sample ASP.NET Core controllers
  Program.cs       - minimal ASP.NET Core host
```

The React application uses **react-hook-form** to manage the form, **html2pdf.js** to export to PDF
and **docxtemplater** to build a DOCX file. See `ClientApp/src/App.jsx` for how the form data affects
the template and how export functions are implemented.
