import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Dashboard } from './pages/Dashboard';
import { CreateAssessment } from './pages/CreateAssessment';
import { AssessmentDetails } from './pages/AssessmentDetails';
import { EditCategory } from './pages/EditCategory';
import { Completion } from './pages/Completion';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/create" element={<CreateAssessment />} />
          <Route path="/assessment/:id" element={<AssessmentDetails />} />
          <Route path="/assessment/:id/category/:category" element={<EditCategory />} />
          <Route path="/assessment/:id/complete" element={<Completion />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;