import React, { useEffect, useState } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Error } from '../components/common/Error';
import { Form, FormField, Input } from '../components/common/Form';
import { Loading } from '../components/common/Loading';
import { organizationApi } from '../api/client';
import type { Organization } from '../types/organization';

export const Organizations: React.FC = () => {
  const [organizations, setOrganizations] = useState<Organization[]>([]);
  const [name, setName] = useState('');
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadOrganizations = async () => {
    try {
      const response = await organizationApi.list();
      setOrganizations(response);
    } catch (err) {
      const message = err instanceof globalThis.Error ? err.message : 'Failed to load organizations';
      setError(message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadOrganizations();
  }, []);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (!name.trim()) {
      setError('Organization name is required');
      return;
    }

    setSubmitting(true);
    setError(null);

    try {
      await organizationApi.create({ name: name.trim() });
      setName('');
      await loadOrganizations();
    } catch (err) {
      const message = err instanceof globalThis.Error ? err.message : 'Failed to create organization';
      setError(message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-2xl font-bold text-gray-900">Organizations</h2>
        <p className="text-sm text-gray-600 mt-1">
          Manage organizations that will run AI readiness assessments.
        </p>
      </div>

      {error && <Error message={error} />}

      <Card>
        <CardHeader>
          <CardTitle>Add Organization</CardTitle>
        </CardHeader>
        <CardContent>
          <Form onSubmit={handleSubmit} className="max-w-xl">
            <FormField label="Name">
              <Input
                type="text"
                value={name}
                onChange={(event) => setName(event.target.value)}
                placeholder="Enter organization name"
                maxLength={120}
                required
              />
            </FormField>
            <Button type="submit" disabled={submitting}>
              {submitting ? <Loading size="sm" /> : 'Add Organization'}
            </Button>
          </Form>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Organization List</CardTitle>
        </CardHeader>
        <CardContent>
          {loading ? (
            <Loading />
          ) : organizations.length === 0 ? (
            <div className="rounded-md border border-dashed border-gray-300 p-6 text-center text-gray-600">
              No organizations found. Add your first organization above.
            </div>
          ) : (
            <div className="space-y-3">
              {organizations.map((organization) => (
                <div
                  key={organization.id}
                  className="flex flex-col gap-2 rounded-md border border-gray-200 bg-white p-4 sm:flex-row sm:items-center sm:justify-between"
                >
                  <div>
                    <p className="font-medium text-gray-900">{organization.name}</p>
                    <p className="text-xs text-gray-500">ID: {organization.id}</p>
                  </div>
                  <p className="text-sm text-gray-600">
                    Created: {new Date(organization.createdAt).toLocaleDateString()}
                  </p>
                </div>
              ))}
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  );
};
