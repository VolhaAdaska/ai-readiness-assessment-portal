import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Form, FormField, Input } from '../components/common/Form';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { assessmentApi } from '../api/client';

export const CreateAssessment: React.FC = () => {
  const [organizationId, setOrganizationId] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!organizationId.trim()) {
      setError('Organization ID is required');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = await assessmentApi.create({ organizationId });
      // After creating, start the assessment
      await assessmentApi.start(response.assessmentId);
      navigate(`/assessment/${response.assessmentId}`);
    } catch (err) {
      setError('Failed to create assessment');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto">
      <Card>
        <CardHeader>
          <CardTitle>Create New Assessment</CardTitle>
        </CardHeader>
        <CardContent>
          {error && <Error message={error} className="mb-4" />}
          <Form onSubmit={handleSubmit}>
            <FormField label="Organization ID">
              <Input
                type="text"
                value={organizationId}
                onChange={(e) => setOrganizationId(e.target.value)}
                placeholder="Enter organization ID"
                required
              />
            </FormField>
            <div className="flex space-x-4">
              <Button type="submit" disabled={loading}>
                {loading ? <Loading size="sm" /> : 'Create Assessment'}
              </Button>
              <Button type="button" variant="outline" onClick={() => navigate('/')}>
                Cancel
              </Button>
            </div>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
};