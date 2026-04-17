import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { AssessmentStatus, getAssessmentStatusLabel, getCategoryTypeLabel } from '../types/assessment';
import type { Assessment } from '../types/assessment';
import { assessmentApi } from '../api/client';

export const AssessmentDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [assessment, setAssessment] = useState<Assessment | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;

    const fetchAssessment = async () => {
      try {
        const data = await assessmentApi.get(id);
        setAssessment(data);
      } catch (err) {
        setError('Failed to load assessment');
      } finally {
        setLoading(false);
      }
    };

    fetchAssessment();
  }, [id]);

  const handleComplete = async () => {
    if (!id) return;

    try {
      await assessmentApi.complete(id);
      // Refresh assessment
      const data = await assessmentApi.get(id);
      setAssessment(data);
    } catch (err) {
      setError('Failed to complete assessment');
    }
  };

  if (loading) return <Loading />;
  if (error) return <Error message={error} />;
  if (!assessment) return <Error message="Assessment not found" />;

  const allComplete = assessment.categories.every(cat => cat.isComplete);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold text-gray-900">
          Assessment {assessment.id}
        </h2>
        {assessment.status === AssessmentStatus.InProgress && allComplete && (
          <Button onClick={handleComplete}>Complete Assessment</Button>
        )}
        {assessment.status === AssessmentStatus.Completed && (
          <Link to={`/assessment/${id}/complete`}>
            <Button>View Results</Button>
          </Link>
        )}
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Assessment Overview</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <p className="text-sm text-gray-600">Organization ID</p>
              <p className="font-medium">{assessment.organizationId}</p>
            </div>
            <div>
              <p className="text-sm text-gray-600">Status</p>
              <p className="font-medium">{getAssessmentStatusLabel(assessment.status)}</p>
            </div>
            <div>
              <p className="text-sm text-gray-600">Created</p>
              <p className="font-medium">{new Date(assessment.createdAt).toLocaleDateString()}</p>
            </div>
            {assessment.completedAt && (
              <div>
                <p className="text-sm text-gray-600">Completed</p>
                <p className="font-medium">{new Date(assessment.completedAt).toLocaleDateString()}</p>
              </div>
            )}
          </div>
        </CardContent>
      </Card>

      <div>
        <h3 className="text-lg font-semibold mb-4">Categories</h3>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {assessment.categories.map((category) => (
            <Card key={category.category}>
              <CardHeader>
                <CardTitle className="flex justify-between items-center">
                  {getCategoryTypeLabel(category.category)}
                  <span className={`px-2 py-1 text-xs rounded ${
                    category.isComplete ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'
                  }`}>
                    {category.isComplete ? 'Complete' : 'Incomplete'}
                  </span>
                </CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm text-gray-600">Maturity Level: {category.maturityLevel}</p>
                <p className="text-sm text-gray-600">Score: {category.categoryScore}%</p>
                <div className="mt-4">
                  <Link to={`/assessment/${id}/category/${category.category}`}>
                    <Button variant="outline" size="sm">
                      {category.isComplete ? 'Edit' : 'Assess'}
                    </Button>
                  </Link>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </div>
  );
};