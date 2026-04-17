import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Form, FormField, Select, Textarea } from '../components/common/Form';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { getCategoryTypeLabel, isCategoryType } from '../types/assessment';
import type { CategoryAssessment } from '../types/assessment';
import { assessmentApi } from '../api/client';

export const EditCategory: React.FC = () => {
  const { id, category } = useParams<{ id: string; category: string }>();
  const navigate = useNavigate();
  const [categoryData, setCategoryData] = useState<CategoryAssessment | null>(null);
  const [maturityLevel, setMaturityLevel] = useState(0);
  const [observations, setObservations] = useState('');
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id || !category) return;

    const parsedCategory = Number(category);
    if (!Number.isInteger(parsedCategory) || !isCategoryType(parsedCategory)) {
      setError('Invalid category');
      setLoading(false);
      return;
    }

    const fetchCategory = async () => {
      try {
        const cat = await assessmentApi.getCategory(id, parsedCategory);
        setCategoryData(cat);
        setMaturityLevel(cat.maturityLevel);
        setObservations(cat.observations);
      } catch (err) {
        setError('Failed to load category');
      } finally {
        setLoading(false);
      }
    };

    fetchCategory();
  }, [id, category]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!id || !category) return;

    const parsedCategory = Number(category);
    if (!Number.isInteger(parsedCategory) || !isCategoryType(parsedCategory)) {
      setError('Invalid category');
      return;
    }

    setSaving(true);
    setError(null);

    try {
      await assessmentApi.updateCategory(id, parsedCategory, {
        maturityLevel,
        observations,
      });
      navigate(`/assessment/${id}`);
    } catch (err) {
      setError('Failed to update category');
    } finally {
      setSaving(false);
    }
  };

  const maturityOptions = [
    { value: '0', label: '0 - Not Assessed' },
    { value: '1', label: '1 - Initial' },
    { value: '2', label: '2 - Developing' },
    { value: '3', label: '3 - Defined' },
    { value: '4', label: '4 - Managed' },
    { value: '5', label: '5 - Optimizing' },
  ];

  if (loading) return <Loading />;
  if (error) return <Error message={error} />;
  if (!categoryData) return <Error message="Category not found" />;

  return (
    <div className="max-w-2xl mx-auto">
      <Card>
        <CardHeader>
          <CardTitle>Assess {getCategoryTypeLabel(categoryData.category)}</CardTitle>
        </CardHeader>
        <CardContent>
          {error && <Error message={error} className="mb-4" />}
          <Form onSubmit={handleSubmit}>
            <FormField label="Maturity Level">
              <Select
                value={maturityLevel.toString()}
                onChange={(e) => setMaturityLevel(parseInt(e.target.value))}
                options={maturityOptions}
              />
            </FormField>
            <FormField label="Observations">
              <Textarea
                value={observations}
                onChange={(e) => setObservations(e.target.value)}
                rows={4}
                placeholder="Enter your observations and notes..."
              />
            </FormField>
            <div className="flex space-x-4">
              <Button type="submit" disabled={saving}>
                {saving ? <Loading size="sm" /> : 'Save Assessment'}
              </Button>
              <Button type="button" variant="outline" onClick={() => navigate(`/assessment/${id}`)}>
                Cancel
              </Button>
            </div>
          </Form>
        </CardContent>
      </Card>
    </div>
  );
};