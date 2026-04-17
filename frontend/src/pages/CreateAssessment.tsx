import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Form, FormField, Input, Select } from '../components/common/Form';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { assessmentApi, organizationApi } from '../api/client';
import type { Organization } from '../types/organization';

export const CreateAssessment: React.FC = () => {
  const [assessmentName, setAssessmentName] = useState('');
  const [organizationId, setOrganizationId] = useState('');
  const [organizations, setOrganizations] = useState<Organization[]>([]);
  const [organizationsLoading, setOrganizationsLoading] = useState(true);
  const [organizationsError, setOrganizationsError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const loadOrganizations = async () => {
      try {
        const response = await organizationApi.list();
        setOrganizations(response);

        if (response.length > 0) {
          setOrganizationId(response[0].id);
        }
      } catch (err) {
        const message = err instanceof globalThis.Error ? err.message : 'Failed to load organizations';
        setOrganizationsError(message);
      } finally {
        setOrganizationsLoading(false);
      }
    };

    loadOrganizations();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!assessmentName.trim()) {
      setError('Assessment name is required');
      return;
    }

    if (!organizationId.trim()) {
      setError('Please select an organization');
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

          {organizationsError && <Error message={organizationsError} className="mb-4" />}

          {organizationsLoading ? (
            <div className="py-6">
              <Loading />
            </div>
          ) : organizations.length === 0 ? (
            <div className="space-y-3 rounded-md border border-dashed border-gray-300 p-4">
              <p className="text-sm text-gray-700">
                No organizations are available. Create an organization before starting an assessment.
              </p>
              <Link to="/organizations">
                <Button type="button">Go to Organizations</Button>
              </Link>
            </div>
          ) : (
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
            <FormField label="Organization">
              <Select
                value={organizationId}
                onChange={(e) => setOrganizationId(e.target.value)}
                options={organizations.map((organization) => ({
                  value: organization.id,
                  label: organization.name,
                }))}
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
          )}
        </CardContent>
      </Card>
    </div>
  );
};