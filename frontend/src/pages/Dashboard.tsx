import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { assessmentApi } from '../api/client';
import { AssessmentStatus, getAssessmentStatusLabel, getReadinessLevelLabel } from '../types/assessment';
import type { AssessmentSummary } from '../types/assessment';

export const Dashboard: React.FC = () => {
  const [assessments, setAssessments] = useState<AssessmentSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchAssessments = async () => {
      try {
        const response = await assessmentApi.list();
        setAssessments(response);
      } catch (err) {
        const message = err instanceof globalThis.Error ? err.message : 'Failed to load assessments';
        setError(message);
      } finally {
        setLoading(false);
      }
    };

    fetchAssessments();
  }, []);

  const formatShortId = (id: string): string => id.split('-')[0] ?? id;

  if (loading) {
    return (
      <div className="py-16">
        <Loading size="lg" />
      </div>
    );
  }

  if (error) {
    return (
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h2 className="text-2xl font-bold text-gray-900">Dashboard</h2>
          <Link to="/create">
            <Button>New Assessment</Button>
          </Link>
        </div>
        <Error message={error} />
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold text-gray-900">Dashboard</h2>
        <Link to="/create">
          <Button>New Assessment</Button>
        </Link>
      </div>

      {assessments.length === 0 ? (
        <Card>
          <CardHeader>
            <CardTitle>No Assessments Yet</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-gray-600 mb-4">
              Start a baseline assessment to track AI readiness progress for your organization.
            </p>
            <Link to="/create">
              <Button>Create First Assessment</Button>
            </Link>
          </CardContent>
        </Card>
      ) : (
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          {assessments.map((assessment) => {
            const isCompleted = assessment.status === AssessmentStatus.Completed;

            return (
              <Card key={assessment.assessmentId}>
                <CardHeader className="flex flex-row items-start justify-between">
                  <div>
                    <CardTitle>Assessment {formatShortId(assessment.assessmentId)}</CardTitle>
                    <p className="text-sm text-gray-500 mt-1">{assessment.assessmentId}</p>
                  </div>
                  <span className="px-2 py-1 text-xs rounded bg-gray-100 text-gray-700">
                    {getAssessmentStatusLabel(assessment.status)}
                  </span>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 text-sm">
                    <div>
                      <p className="text-gray-500">Organization</p>
                      <p className="font-medium text-gray-900 break-all">{assessment.organizationId}</p>
                    </div>
                    <div>
                      <p className="text-gray-500">Created</p>
                      <p className="font-medium text-gray-900">
                        {new Date(assessment.createdAt).toLocaleDateString()}
                      </p>
                    </div>
                    <div>
                      <p className="text-gray-500">Readiness Level</p>
                      <p className="font-medium text-gray-900">
                        {assessment.readinessLevel === undefined
                          ? 'In Progress'
                          : getReadinessLevelLabel(assessment.readinessLevel)}
                      </p>
                    </div>
                    <div>
                      <p className="text-gray-500">Overall Score</p>
                      <p className="font-medium text-gray-900">
                        {assessment.overallScore === undefined ? 'N/A' : `${Math.round(assessment.overallScore)}%`}
                      </p>
                    </div>
                  </div>

                  <div>
                    <div className="flex justify-between text-sm mb-1">
                      <span className="text-gray-600">
                        Categories: {assessment.completedCategories}/{assessment.totalCategories}
                      </span>
                      <span className="font-medium text-gray-800">{assessment.completionPercentage}%</span>
                    </div>
                    <div className="w-full h-2 bg-gray-200 rounded-full overflow-hidden">
                      <div
                        className="h-full bg-blue-600"
                        style={{ width: `${assessment.completionPercentage}%` }}
                      />
                    </div>
                  </div>

                  <div className="pt-1">
                    <Link
                      to={
                        isCompleted
                          ? `/assessment/${assessment.assessmentId}/complete`
                          : `/assessment/${assessment.assessmentId}`
                      }
                    >
                      <Button variant={isCompleted ? 'secondary' : 'primary'}>
                        {isCompleted ? 'View Summary' : 'Continue Assessment'}
                      </Button>
                    </Link>
                  </div>
                </CardContent>
              </Card>
            );
          })}
        </div>
      )}
    </div>
  );
};