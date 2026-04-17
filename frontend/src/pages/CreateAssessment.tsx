import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Form, FormField, Input } from '../components/common/Form';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { assessmentApi } from '../api/client';

const GUID_REGEX =
  /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;

export const CreateAssessment: React.FC = () => {
  const [assessmentName, setAssessmentName] = useState('');
  const [organizationId, setOrganizationId] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!assessmentName.trim()) {
      setError('Assessment name is required');
      return;
    }

    if (!organizationId.trim()) {
      setError('Organization ID is required');
      return;
    }

    if (!GUID_REGEX.test(organizationId.trim())) {
      setError('Organization ID must be a valid GUID');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // Backend MVP contract currently accepts only organizationId.
      const response = await assessmentApi.create({ organizationId: organizationId.trim() });
      // After creating, start the assessment
      await assessmentApi.start(response.assessmentId);
      navigate(`/assessment/${response.assessmentId}`);
    } catch (err) {
      const message = err instanceof Error ? err.message : 'Failed to create assessment';
      setError(message);
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
            <FormField label="Assessment Name">
              <Input
                type="text"
                value={assessmentName}
                onChange={(e) => setAssessmentName(e.target.value)}
                placeholder="e.g. Q2 2026 Baseline"
                maxLength={120}
                required
              />
            </FormField>
            <FormField label="Organization ID">
              <Input
                type="text"
                value={organizationId}
                onChange={(e) => setOrganizationId(e.target.value)}
                placeholder="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
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