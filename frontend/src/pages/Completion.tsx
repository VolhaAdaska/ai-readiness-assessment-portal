import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { ReadinessLevel } from '../types/assessment';
import type { Assessment, ReadinessLevel as ReadinessLevelType } from '../types/assessment';
import { assessmentApi } from '../api/client';

export const Completion: React.FC = () => {
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
        setError('Failed to load assessment results');
      } finally {
        setLoading(false);
      }
    };

    fetchAssessment();
  }, [id]);

  if (loading) return <Loading />;
  if (error) return <Error message={error} />;
  if (!assessment) return <Error message="Assessment not found" />;

  const getReadinessColor = (level: ReadinessLevelType) => {
    switch (level) {
      case ReadinessLevel.Critical: return 'text-red-600';
      case ReadinessLevel.Low: return 'text-orange-600';
      case ReadinessLevel.Moderate: return 'text-yellow-600';
      case ReadinessLevel.Good: return 'text-blue-600';
      case ReadinessLevel.Excellent: return 'text-green-600';
      default: return 'text-gray-600';
    }
  };

  return (
    <div className="space-y-6">
      <div className="text-center">
        <h2 className="text-3xl font-bold text-gray-900 mb-2">
          Assessment Complete!
        </h2>
        <p className="text-gray-600">
          Here are your AI readiness results and recommendations.
        </p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Overall Readiness Score</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="text-center">
            <div className="text-6xl font-bold text-blue-600 mb-2">
              {assessment.overallScore}%
            </div>
            <div className={`text-xl font-semibold ${getReadinessColor(assessment.readinessLevel ?? ReadinessLevel.Critical)}`}>
              {assessment.readinessLevel ?? 'Not Classified'}
            </div>
          </div>
        </CardContent>
      </Card>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <Card>
          <CardHeader>
            <CardTitle>Category Scores</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-3">
              {assessment.categories.map((category) => (
                <div key={category.category} className="flex justify-between items-center">
                  <span className="text-sm font-medium">{category.category}</span>
                  <span className="text-sm text-gray-600">{category.categoryScore}%</span>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Recommendations</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {assessment.recommendations.map((rec) => (
                <div key={rec.id} className="border-l-4 border-blue-500 pl-4">
                  <div className="flex items-center justify-between mb-1">
                    <h4 className="text-sm font-medium">{rec.title}</h4>
                    <span className={`px-2 py-1 text-xs rounded ${
                      rec.priority === 'Critical' ? 'bg-red-100 text-red-800' :
                      rec.priority === 'High' ? 'bg-orange-100 text-orange-800' :
                      'bg-yellow-100 text-yellow-800'
                    }`}>
                      {rec.priority}
                    </span>
                  </div>
                  <p className="text-sm text-gray-600">{rec.description}</p>
                  <p className="text-xs text-gray-500 mt-1">Category: {rec.category}</p>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>

      <div className="text-center">
        <Link to="/">
          <Button>Back to Dashboard</Button>
        </Link>
      </div>
    </div>
  );
};